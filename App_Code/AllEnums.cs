using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AllEnums
{

    public static class EnumConverter
    {

        public static string[] ToNameArray<T>()
        {
            return Enum.GetNames(typeof(T)).ToArray();
        }

        public static Array ToValueArray<T>()
        {
            return Enum.GetValues(typeof(T));
        }

        public static List<T> ToListOfValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }


        public static IEnumerable<T> ToEnumerable<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

    }


    public class LogOperation
    {
        public enum Events { Error = 0, Insert = 1, Update = 2, Delete = 3, Register = 4, Login = 5, Logout = 6, ChangePassword = 7, View = 8, ActionConfirmation = 9, CheckoutConfirmation = 10, RechargeAccountCredit = 11, RechargeAccountCash = 12, SendNewsLetter = 13, ApproveUserAccount = 14 }

        public static string OperationPersianName(Events operation)
        {
            string retVal = string.Empty;
            switch (operation)
            {
                case Events.Insert:
                    retVal = "درج";
                    break;
                case Events.Update:
                    retVal = "بروز رسانی";
                    break;
                case Events.Delete:
                    retVal = "حذف";
                    break;
                case Events.Register:
                    retVal = "ثبت نام";
                    break;
                case Events.Login:
                    retVal = "ورود به سایت";
                    break;
                case Events.Logout:
                    retVal = "خروج از سایت";
                    break;
                case Events.ChangePassword:
                    retVal = "تغییر کلمه عبور";
                    break;
                case Events.View:
                    retVal = "گزارش و مشاهده";
                    break;
                case Events.ActionConfirmation:
                    retVal = "تایید عملیات";
                    break;
                case Events.CheckoutConfirmation:
                    retVal = "تایید تسویه حساب";
                    break;
                case Events.RechargeAccountCash:
                    retVal = "شارژ مبلغ حساب";
                    break;
                case Events.RechargeAccountCredit:
                    retVal = "شارژ اعتبار حساب";
                    break;
                case Events.SendNewsLetter:
                    retVal = "ارسال خبرنامه";
                    break;
                case Events.ApproveUserAccount:
                    retVal = "تائید حساب کاربری";
                    break;
                case Events.Error:
                    retVal = "خطا در نرم افزار";
                    break;
            }
            return retVal;
        }
    }
    public class AccessMethods
    {
        public enum Permission
        {
            Insert = 1, Update = 2, Delete = 3, View = 4,
            ChangePassword = 5, DataConfirmation = 6, CheckoutConfirmation = 7, RechargeAccountCredit = 8, RechargeAccountCash = 9, ApproveUserAccount = 10,
            OrderInCheckQueue = 11, OrderConfirmation = 12, OrderPreparation = 13, OrderInDistributionCenter = 14, OrderDistributorAgent = 15, OrderDelivered = 16, SendingNewsLetter = 17
        }

        public static string PermissionPersianName(Permission accessLevel)
        {
            string retVal = string.Empty;
            switch (accessLevel)
            {
                case Permission.Insert:
                    retVal = "مجوز درج";
                    break;
                case Permission.Update:
                    retVal = "مجوز بروز رسانی";
                    break;
                case Permission.Delete:
                    retVal = "مجوز حذف";
                    break;
                case Permission.View:
                    retVal = "مجوز مشاهده";
                    break;
                case Permission.ChangePassword:
                    retVal = "مجوز تغییر کلمه عبور";
                    break;
                case Permission.DataConfirmation:
                    retVal = "مجوز تایید داده ها";
                    break;
                case Permission.CheckoutConfirmation:
                    retVal = "مجوز تایید تسویه حساب";
                    break;
                case Permission.RechargeAccountCash:
                    retVal = "مجوز شارژ مبلغ حساب";
                    break;
                case Permission.RechargeAccountCredit:
                    retVal = "مجوز شارژ اعتبار حساب";
                    break;
                case Permission.ApproveUserAccount:
                    retVal = "مجوز تائید حساب کاربری";
                    break;
                case Permission.OrderInCheckQueue:
                    retVal = "اعمال وضعیت سفارش در صف بررسی";
                    break;
                case Permission.OrderConfirmation:
                    retVal = "اعمال وضعیت تائید سفارش";
                    break;
                case Permission.OrderPreparation:
                    retVal = "اعمال وضعیت آماده سازی سفارش";
                    break;
                case Permission.OrderInDistributionCenter:
                    retVal = "اعمال وضعیت سفارش در مرکز توزیع";
                    break;
                case Permission.OrderDistributorAgent:
                    retVal = "اعمال وضعیت سفارش تحویل مامور توزیع";
                    break;
                case Permission.OrderDelivered:
                    retVal = "اعمال وضعیت سفارش تحویل شد";
                    break;
                case Permission.SendingNewsLetter:
                    retVal = "مجوز ارسال خبرنامه";
                    break;
            }
            return retVal;
        }
    }





    public class UserInfo
    {
        public enum Gender { Damen = 0, Herren = 1, UnKnown = 3 }
        public static string GenderEnglishName(Gender gender)
        {
            string retVal = "";
            switch (gender)
            {
                case Gender.Damen:
                    retVal = "FEMALE";
                    break;
                case Gender.Herren:
                    retVal = "MALE";
                    break;
                case Gender.UnKnown:
                    retVal = string.Empty;
                    break;
                default:
                    break;
            }
            return retVal;
        }

        public static Gender GenderDetuchName(string gender)
        {
            Gender retVal = Gender.UnKnown;
            switch (gender)
            {
                case "FEMALE":
                    retVal = Gender.Damen;
                    break;
                case "MALE":
                    retVal = Gender.Herren;
                    break;
                default:
                    retVal = Gender.UnKnown;
                    break;
            }
            return retVal;
        }
    }


    public class Product
    {
        public enum Inventory { Exist = 1, NotExist = 2 };

    }




    public class Utility
    {
        public static DataTable GetEnumEnglish(string EnumClass, string EnumName)
        {
            DataTable retVal = new DataTable();
            retVal.Columns.Add("Id");
            retVal.Columns.Add("Title");
            Type type = Type.GetType("AllEnums." + EnumClass);
            var methodInfo = type.GetMethod(EnumName + "PersianName");
            Type TEnum = Type.GetType("AllEnums." + EnumClass + "+" + EnumName);
            var values = Enum.GetValues(TEnum);
            var names = Enum.GetNames(TEnum);
            for (int i = 0; i < names.Length; i++)
            {
                var row = retVal.NewRow();
                row["Id"] = Convert.ToInt32(Enum.Parse(TEnum, Convert.ToString(values.GetValue(i))));
                object classInstance = Activator.CreateInstance(type, null);
                if (methodInfo != null)
                    row["Title"] = methodInfo.Invoke(classInstance, new object[] { values.GetValue(i) });
                retVal.Rows.Add(row);
            }
            return retVal;//
        }

        public static string[] GetEnumNames(string EnumClass, string EnumName)
        {
            Type TEnum = Type.GetType("AllEnums." + EnumClass + "+" + EnumName);
            return Enum.GetNames(TEnum);
        }

        public static Array GetEnumValues(string EnumClass, string EnumName, string EnumValue)
        {
            Type TEnum = Type.GetType("AllEnums." + EnumClass + "+" + EnumName + "+" + EnumValue);
            return Enum.GetValues(TEnum);
        }
    }


}
