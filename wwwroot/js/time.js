String.prototype.toPersian = String.prototype.toFaDigit = function (a) {
    return this.replace(/\d+/g, function (digit) {
        var digitArr = [], pDigitArr = [];
        for (var i = 0, len = digit.length; i < len; i++) {
            digitArr.push(digit.charCodeAt(i));
        }

        for (var j = 0, leng = digitArr.length; j < leng; j++) {
            pDigitArr.push(String.fromCharCode(digitArr[j] + ((!!a && a == true) ? 1584 : 1728)));
        }

        return pDigitArr.join('');
    });
};
String.prototype.toEnDigit = function () {
    return this.replace(/[\u06F0-\u06F9]+/g, function (digit) {
        var digits = [], ENDigit = [];
        for (var i = 0, len = digit.length; i < len; i++) {
            digits.push(digit.charCodeAt(i));
        }

        for (var j = 0, leng = digits.length; j < leng; j++) {
            ENDigit.push(String.fromCharCode(digits[j] - 1728));
        }

        return ENDigit.join('');
    });
};

function second_check($ID, $Check_No, $NextID, $Check_No_next) {
    $value_1 = $($ID).html().toString().toEnDigit();
    $value_2 = $($NextID).html().toString().toEnDigit();
    if ($value_1 == 0 && $value_2 == 0) {
        $($ID).html($Check_No.toString().toFaDigit());
        $($NextID).html($Check_No_next.toString().toFaDigit());

        minutes_check($ID.parents('.clock').find(".m2"), 9, $ID.parents('.clock').find(".m1"), 5)
    }
    else {
        $value_1--;
        $value_2--;


        if ($value_1 >= 0) {
            $($ID).html($value_1.toString().toFaDigit());
        }
        else {
            $($ID).html($Check_No.toString().toFaDigit());
            $($NextID).html($value_2.toString().toFaDigit());
        }
    }
}

function minutes_check($ID, $Check_No, $NextID, $Check_No_next) {
    $value_1 = $($ID).html().toString().toEnDigit();
    $value_2 = $($NextID).html().toString().toEnDigit();
    if ($value_1 == 0 && $value_2 == 0) {
        $($ID).html($Check_No.toString().toFaDigit());
        $($NextID).html($Check_No_next.toString().toFaDigit());

        hour_check($ID.parents('.clock').find(".h2"), 9, $ID.parents('.clock').find(".h1"), 2)
    }
    else {
        $value_1--;
        $value_2--;

        if ($value_1 >= 0) {
            $($ID).html($value_1.toString().toFaDigit());
        }
        else {
            $($ID).html($Check_No.toString().toFaDigit());
            $($NextID).html($value_2.toString().toFaDigit());
        }
    }
}
function hour_check($ID, $Check_No, $NextID, $Check_No_next) {
    $value_1 = $($ID).html().toString().toEnDigit();
    $value_2 = $($NextID).html().toString().toEnDigit();

    if ($value_1 == 0 && $value_2 == 0) {
        $($ID).html("3".toString().toFaDigit());
        $($NextID).html($Check_No_next.toString().toFaDigit());
        day_check($ID.parents('.clock').findn(".d2"), 9, $ID.parents('.clock').find(".d1"), 2)
    }
    else {
        $value_1--;
        $value_2--;

        if ($value_1 >= 0) {
            $($ID).html($value_1.toString().toFaDigit());
        }
        else {
            $($ID).html($Check_No.toString().toFaDigit());
            $($NextID).html($value_2.toString().toFaDigit());
        }
    }
}
function day_check($ID, $Check_No, $NextID, $Check_No_next) {
    $value_1 = $($ID).html().toString().toEnDigit();
    $value_2 = $($NextID).html().toString().toEnDigit();

    if ($value_1 == 0 && $value_2 == 0) {
        $($ID).html($Check_No.toString().toFaDigit());
        $($NextID).html($Check_No_next.toString().toFaDigit());
        location.reload();
    }
    else {
        $value_1--;
        $value_2--;

        if ($value_1 >= 0) {
            $($ID).html($value_1.toString().toFaDigit());
        }
        else {
            $($ID).html($Check_No.toString().toFaDigit());
            $($NextID).html($value_2.toString().toFaDigit());
        }
    }
}

setInterval(function () {
    $('.clock').each(function () {
        second_check($(this).find(".s2"), 9, $(this).find(".s1"), 5);
    });
},1000);