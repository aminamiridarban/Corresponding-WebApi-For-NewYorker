/*Item Info*/
var SearchingIsBusy = false,
    temp = "",
    cookie,
    Paging_PageNo = 1,
    PageTitle,
    Paging_PageCount = 12,
    minPrice,
    maxPrice,
    timer,
    sliderValues = [4000, 3000, 2000, 1000, 900, 800, 700, 600, 500, 450, 400, 350, 300, 250, 200, 150, 100, 90, 85, 80, 75, 70, 60, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 9, 8, 7, 6 / 5, 6, 5 / 5, 5, 4 / 5, 4, 3 / 5, 2, 1, 0];

/*Call Paging Plugin In Your Page*/
$(document).ready(function () {
    page = "List";
    PageURL = window.location.href;
    if ($.contains(PageURL, 'page-')) {
        Paging_PageNo = PageURL.substring(PageURL.toLowerCase().indexOf("page-") + "page-".length);
        Paging_PageNo = Paging_PageNo.replace("/", "");
        PageTitle = $('title').html();
        PageTitle = PageTitle.split(" Page");
        $('title').html(PageTitle[0] + " Page" + Paging_PageNo);
    }
    $(".listview").click(function () {
        goToListView($(this));
    });
    $(".gridview").click(function () {
        goToGridView($(this));
    });

    getProductItemsSorting();
    SetSortEventAndPosition();


    $(".filter-dynamic > li").bind("click", function () {
        setFiltering($(this));
    });


    bindTypeEvents();

    $(window).bind("hashchange", function () {
        doSearch();
    });

    filteringHandller();

    if (location.href.indexOf("#!/") != -1 && location.hash != "") {
        doSearch()
    }


    minPrice = 0;
    maxPrice = sliderValues[0];
    $("#prSlider").slider({
        min: 0,
        max: sliderValues.length - 1,
        step: 1,
        range: true,
        animate: true,
        values: [0, sliderValues[0]],
        slide: function (d, e) {
            $("#prSlider #amount .minprice").html(formatCurrency(sliderValues[e.values[1]], "<span class='aadAjaxLoaderColors'> €</span>"));
            $("#prSlider #amount .maxprice").html(formatCurrency(sliderValues[e.values[0]], "<span class='aadAjaxLoaderColors'> €</span>"));
            minPrice = sliderValues[e.values[1]];
            maxPrice = sliderValues[e.values[0]]
        }
    });

    initializeFilteringSelectors();

    $("#prSlider a:odd").addClass("right").mouseup(function () {
        $(this).removeClass("ui-state-focus");
        window.setTimeout(function () {
            selectMinPrice()
        }, 500)
    });
    $("#prSlider a:even").addClass("left").removeClass("ui-state-focus").mouseup(function () {
        $(this).removeClass("ui-state-focus");
        window.setTimeout(function () {
            selectMaxPrice()
        }, 500)
    });
    $("body").mouseup(function (f) {
        var d = $(f.target);
        if (!d.is(".ui-slider-handle")) {
            $("#prSlider .ui-state-focus").mouseup()
        }
    })
    doSearch();
});

function goToListView(a) {
    a.addClass("active");
    $(".gridView").removeClass("active");
    $(".productlist").removeClass("gridlike").addClass("listlike");
    $.cookie("NewYorker_sorting", "list", {
        expires: 365,
        path: "/"
    })
}

function goToGridView(a) {
    a.addClass("active");
    $(".listView").removeClass("active");
    $(".productlist").removeClass("listlike").addClass("gridlike");
    $.cookie("NewYorker_sorting", "grid", {
        expires: 365,
        path: "/"
    });
}

function getProductItemsSorting() {
    cookie = $.cookie("NewYorker_sorting");
    if (cookie == "list") {
        goToListView($(".listView"))
    } else {
        if (cookie == "grid") {
            goToGridView($(".gridView"));
        }
    }
}

function SetSortEventAndPosition() {
    $(".sort-by").change(function () {
        resetPaging();
        opts = $(this)[0].options;
        $.map(opts, function (b) {
            location.hash = location.hash.replace(b.value + "/", "")
        });
        location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + $(this).val() + "/"
    });
    $(".sort-condition").change(function () {
        resetPaging();
        opts = $(this)[0].options;
        $.map(opts, function (b) {
            location.hash = location.hash.replace(b.value + "/", "")
        });
        if ($(this).val().indexOf("0") == -1) {
            location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + $(this).val() + "/"
        }
    })
}

function resetPaging() {
    PageURL = window.location.hash;
    if ($.contains(PageURL, 'page-')) {
        location.hash = PageURL.replace("page-" + Paging_PageNo + "/", "");
        PageTitle = $('title').html();
        PageTitle = PageTitle.replace("Page" + Paging_PageNo, "");
        $('title').html(PageTitle);
    }
}

function setFiltering(a) {
    if (SearchingIsBusy) {
        return
    }
    resetPaging();
    temp = "";
    temp2 = a.attr("id") + "/";
    if (a.hasClass("selected")) {
        location.hash = location.hash.replace(temp, "");
        location.hash = location.hash.replace(temp2, "")
    } else {
        location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + temp;
        location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + temp2
    }
}

function productOptions(b) {
    resetPaging();
    var a = "checkbox-icon-checked";
    if (b.closest("a").hasClass(a)) {
        temp = b.val() + "/";
        location.hash = location.hash.replace(temp, "");
        $(this).closest("a").removeClass(a);
        initializeFilteringSelectors(true, false);
    } else {
        location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + b.val() + "/";
        $(this).closest("a").addClass(a);
        initializeFilteringSelectors(true, true);
    }
}
function productOptionsRDB(b) {
    $('.rdbDelAll input').prop('checked', false);
    resetPaging();
    var a = "checkbox-icon-checked";
    if (b.closest("a").hasClass(a)) {
    } else {
        _OldRdb = b.parents('.pDdl')
        temp = _OldRdb.find('.' + a).removeClass(a).children('input').val() + "/";
        location.hash = location.hash.replace(temp, "") + (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + b.val() + "/";;
        $(this).closest("a").addClass(a);
        initializeFilteringSelectors(true, true);
    }
}

function viewallClick(c, a) {
    c.addClass("selected");
    var b = c.closest(".prd_crap_box");
    b.find(".filter-dynamic li").each(function () {
        $(this).removeClass("selected")
    });
    b.removeClass("has-selections");
    if (!a) {
        locationHash = location.hash;
        temp = "";
        b.find("ul.chosen li").map(function () {
            temp = $(this).attr("id") + "/"
            locationHash = locationHash.replace(temp, "");
        });
        location.hash = locationHash;
    }
    b.find("ul.chosen").hide().html("");
}

function initializeFilteringSelectors(g, a) {
    var e = new Array();
    var h = "checkbox-icon-checked";
    var j = sliderValues.length - 1,
        k = 0;
    if (window.location.href.indexOf("#!/") != -1) {
        locationHash = window.location.hash
    } else {
        locationHash = window.location.href.toLowerCase().substring(window.location.href.toLowerCase().indexOf("view/") + "view/".length)
    }
    if (g) {

        $("input[id^='ChkComingSoon']").closest("a").removeClass(h);
        $("input[id^='ChkSale']").closest("a").removeClass(h);
        $("input[id^='ChkIsNew']").closest("a").removeClass(h);
        $("input[id^='ChkDisscount']").closest("a").removeClass(h);

        $("input[id^='rdbSortMostexpensive']").closest("a").removeClass(h);
        $("input[id^='rdbSortCheapest']").closest("a").removeClass(h);
        $("input[id^='rdbSortPublishDate']").closest("a").removeClass(h);
        $("input[id^='rdbSortNewest']").closest("a").removeClass(h);


        $(".prd_crap_list").children("li").each(function () {
            var i = $(this).find(".viewall");
            viewallClick(i, true)
        });
    }
    e = locationHash.replace("#!/", "").split("/");
    for (var c in e) {
        if (e[c].search("comingsoon") != -1) {
            var d = $(".TMComingSoonFilter input:checkbox[value='" + e[c] + "']");
            d.closest("a").addClass(h);
        } else {
            if (e[c].search("sale") != -1) {
                var d = $(".TMIsSaleFilter input:checkbox[value='" + e[c] + "']");
                d.closest("a").addClass(h);
            } else {
                if (e[c].search("isnew") != -1) {
                    var d = $(".TMIsNewFilter input:checkbox[value='" + e[c] + "']");
                    d.closest("a").addClass(h);
                } else {
                    if (e[c].search("disscount") != -1) {
                        var d = $(".TMDisscountFilter input:checkbox[value='" + e[c] + "']");
                        d.closest("a").addClass(h);
                    } else {
                        if (e[c].search("sortby") != -1) {
                            var d = $(".aadAjaxLoaderSortFilter input:checkbox[value='" + e[c] + "']");
                            d.closest("a").addClass(h);
                        } else {
                            if (e[c].search("pagesize") != -1) {
                                $(".page-size").val(e[c])
                            } else {
                                if (e[c].search("minprice") != -1) {
                                    j = sliderValues.indexOf(parseInt(e[c].replace("minprice-", "")))
                                } else {
                                    if (e[c].search("maxprice") != -1) {
                                        k = sliderValues.indexOf(parseInt(e[c].replace("maxprice-", "")))
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


function InitialSlider() {
    $("#prSlider").slider({
        min: 0,
        max: sliderValues.length - 1,
        values: [k, j]
    });

    $("#prSlider #amount .minprice").html(formatCurrency(sliderValues[j], "<span class='aadAjaxLoaderColors'> €</span>"));
    $("#prSlider #amount .maxprice").html(formatCurrency(sliderValues[k], "<span class='aadAjaxLoaderColors'> €</span>"));
}


function optionsClick(b) {
    if (SearchingIsBusy) {
        return
    }
    var a = b.closest(".prd_crap_box");
    b.toggleClass("selected");
    if (b.hasClass("selected")) {
        a.find("ul.chosen").append("<li id='" + b.attr("id") + "' >" + b.text() + "</li>");
        bindAttributeEvents(a);
    } else {
        a.find("ul.chosen li").remove("#" + b.attr("id"));
        temp = b.children().attr("id") + "/";
        location.hash = location.hash.replace(temp, "");
    }
    if (a.find("ul.chosen").children().length) {
        a.find("div.viewall").removeClass("selected");
        a.addClass("has-selections")
    } else {
        a.removeClass("has-selections");
        a.find("div.viewall").addClass("selected");
    }
}

function bindAttributeEvents(a) {
    $(".aadAjaxLoaderSelectedFilters > li > a").bind("click", function () {
        if (SearchingIsBusy) {
            return
        }
        temp = $(this).attr("id") + "/";
        location.hash = location.hash.replace(temp, "");
        a.find("ul.chosen").children("#" + $(this).attr("id")).remove();
        $(this).removeClass("selected");
        if (!a.find("ul.chosen").children().length) {
            a.find(".viewall").addClass("selected");
            a.removeClass("has-selections");
        }
    });
}

function doSearch() {
    disableForm();
    SearchingIsBusy = true;
    var b = "";
    var a = (window.location == window.parent.location);
    if (!location.hash.lastIndexOf("/") == location.hash.length - 1) {
        location.hash = location.hash + "/"
    }
    b = ((!a) ? "Frame" : "") + window.location.href.substring(window.location.href.toLowerCase().indexOf("view/") + "view/".length).replace("#!/", "");

    if (a) {
        disableForm();
    }

    var data = { 'urlString': window.location.href };

    $.ajax({
        type: "POST",
        url: siteRoot + "/api/webapi.asmx/GetProductsByFilterString",
        data: JSON.stringify(data),
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (result) {
            if (result.d.filteredProductsHtmlString.length > 0) {
                $(".productlist").html(result.d.filteredProductsHtmlString);
                $("#aadAjaxLoaderPaging").html(result.d.pager);
                $(".aadAjaxLoaderSelectedFilters").html(result.d.filterString);
                $(".aadAjaxLoaderSelectedFilters a").bind("click", function () {
                    BreadCrumbCloseClick($(this));
                });
          
                //RANDOM Special Offer desiered to be alerted to the users
                alertify.alert()
                    .setting({
                        'title': '❤ Lucky YoOoOoU ❤',
                        'label': 'Ich habe das verstanden',
                        'message': '<h1>Dank der Beratung zu unserem speziellen Zufalls Angebot haben Sie Glück</h1><br/> ' +
                            //As I mentioned in comments, In last lines before return values in 'GetProductsByFilterString' method.
                            //We could just produce the random item here from results.
                            $(".productlist li.randomElement").html(),
                        'onok': function () { }
                    }).show();
            } else {
                $(".productlist").html("<div class='t-center title' style='padding:10.3% 0'>There is nothing found..!</div>");
            }
           
            enableForm();
            SearchingIsBusy = false;
        },
        error: function (e, c, d) {
            alertify.error("We have some issue to fetching data right now, Please check out few minutes later.");
            if (c === "timeout") {
                $(".aadAjaxLoader").html("<span id='spLoading'>Loading (20 Seconds Remaining)</span><span id='spCancel'>Ignore</span>");
                reloadSearch(7);
                $("#spCancel").bind("click", function () {
                    enableForm();
                    window.clearTimeout(timer);
                })
            }
            if (c === "parsererror") {
                window.location.href = window.location.href.remove("/view/#!/") + "/view/#!/";
            }
        },
        
    })
}

function disableForm() {
    $(".aadAjaxLoaderPane").css("pointer-events", "none").find("*").attr("disabled", "disabled");
    $('.aadAjaxLoader').html('').show();
}

function enableForm() {
    $(".aadAjaxLoaderPane").css("pointer-events", "").find("*").removeAttr("disabled");
    $('.aadAjaxLoader').html('').hide();
}

function BreadCrumbCloseClick(a) {
    if (SearchingIsBusy) {
        return
    }
    location.hash = location.hash.replace(a.attr("id").trim(), "");
    initializeFilteringSelectors(true)
}

function reloadSearch(a) {
    window.clearTimeout(timer);
    timer = setTimeout(function () {
        if (a >= 0) {
            $(".aadAjaxLoader #spLoading").text("Loading (" + a + " Seconds)");
            a--;
            reloadSearch(a)
        } else {
            $(".aadAjaxLoader").hide();
            doSearch();
        }
    }, 1000)
}

function bindTypeEvents() {
    $("input[id^='ChkComingSoon']").bind("click", function () {
        productOptions($(this));
    });
    $("input[id^='ChkSale']").bind("click", function () {
        productOptions($(this));
    });
    $("input[id^='ChkIsNew']").bind("click", function () {
        productOptions($(this));
    });
    $("input[id^='ChkDisscount']").click(function () {
        productOptions($(this));
    });

    $("input[id^='rdbSortMostexpensive']").click(function () {
        productOptionsRDB($(this));
    });
    $("input[id^='rdbSortCheapest']").click(function () {
        productOptionsRDB($(this));
    });

    $("input[id^='rdbSortPublishDate']").click(function () {
        productOptionsRDB($(this));
    });
    $("input[id^='rdbSortNewest']").click(function () {
        productOptionsRDB($(this));
    });

    $("input[id^='inpSearchProductsAjax']").click(function () {
        window.location.href = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + "#!/allproducts/keyword-" + $("#txtSearchProductAjax").val() + "/";
    });

    $("select[id^='ddlPageSize']").change(function () {
        window.location.href = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + "#!/pagesize-" + $("#ddlPageSize").val() + "/";
    });
}

function filteringHandller() {
    var a;
    var b = null;
    options = $(".filter-dynamic li");
    viewall = $(".prd_crap_box .viewall");
    options.bind("click", function () {
        optionsClick($(this))
    });
    viewall.bind("click", function () {
        viewallClick($(this));
        return false;
    })
}

function selectMaxPrice() {
    resetPaging();
    if (maxPrice != undefined) {
        if (location.hash.indexOf("maxprice-") == -1) {
            location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + "maxprice-" + maxPrice + "/"
        } else {
            var a = new Array();
            a = location.hash.split("/");
            for (var b in a) {
                if (a[b].search("maxprice") != -1) {
                    if (maxPrice != sliderValues[0]) {
                        location.hash = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + location.hash.replace(a[b], "maxprice-" + maxPrice)
                    } else {
                        location.hash = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + location.hash.replace(a[b] + "/", "")
                    }
                }
            }
        }
    }
}

function selectMinPrice() {
    resetPaging();
    if (minPrice != undefined) {
        if (location.hash.indexOf("minprice-") == -1) {
            location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + "minprice-" + minPrice + "/"
        } else {
            var a = new Array();
            a = location.hash.split("/");
            for (var b in a) {
                if (a[b].search("minprice") != -1) {
                    if (minPrice != 0) {
                        location.hash = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + location.hash.replace(a[b], "minprice-" + minPrice)
                    } else {
                        location.hash = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + location.hash.replace(a[b] + "/", "")
                    }
                }
            }
        }
    }
}

function formatCurrency(a, c) {
    a = a.toString().replace(/\$|\,/g, "");
    if (isNaN(a)) {
        a = "0"
    }
    sign = (a == (a = Math.abs(a)));
    a = Math.floor(a * 100 + 0.50000000001);
    a = Math.floor(a / 100).toString();
    for (var b = 0; b < Math.floor((a.length - (1 + b)) / 3); b++) {
        a = a.substring(0, a.length - (4 * b + 3)) + "," + a.substring(a.length - (4 * b + 3))
    }
    return (((sign) ? "" : "-") + a + " " + c)
}

function BreadCrumbCloseClick(a) {
    if (SearchingIsBusy) {
        return
    }
    location.hash = location.hash.replace(a.attr("id").trim() + '/', "");
    initializeFilteringSelectors(true)
}

function JumpToPage(a) {
    locationHash = window.location.hash;
    if (locationHash.indexOf("page-") == -1) {
        Paging_PageNo = a;
        location.hash += (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + "page-" + Paging_PageNo + "/"
    } else {
        Paging_PageNo = a;
        var d = locationHash.indexOf("page-") + "page-".length;
        var c = locationHash.indexOf("/", d) - 1;
        var b = locationHash.substring(d, d + c);
        location.hash = (location.hash.lastIndexOf("/") == location.hash.length - 1 ? "" : "/") + location.hash.replace("page-" + b, "page-" + Paging_PageNo + "/")
    }
    return false
}








