using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BankManage;
using BankManage.money;
using System.Windows;

namespace BankManage.common
{
    public class DataOperation
    {
        public static readonly string[] AccountType = { "活期存款", "定期存款", "零存整取" };
        /// <summary>
        /// 获取操作员姓名
        /// </summary>
        /// <param name="id">操作员编号</param>
        /// <returns></returns>
        public static string GetOperateName(string id)
        {
            using (BankEntities c = new BankEntities())
            {
                //查询传入id的记录
                var q = from t in c.EmployeeInfo
                         where t.EmployeeNo == id
                         select t;

                //若存在记录，获取第一条的姓名信息并返回
                if (q != null && q.Count()>=1)
                {
                    return q.First().EmployeeName;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 根据存款类型创建开户账号，默认为活期类型
        /// </summary>
        /// <param name="accountType">存款类型</param>
        /// <returns></returns>
        public static Custom CreateCustom(string accountType, string rateType)
        {
           
            Custom custom = null;
            switch (accountType)
            {
                case "活期存款":
                    //创建活期存款类
                    custom = new CustomChecking();
                    custom.AccountInfo.accountType = accountType;
                    custom.AccountInfo.rateType = "活期";
                    break;
                case "定期存款":
                    //创建定期存款类
                    custom = new CustomFixed();
                    custom.AccountInfo.accountType = accountType;
                    custom.AccountInfo.rateType = rateType;
                    break;
                case "零存整取":
                    //创建零存整取类
                    custom = new CustomWhole();
                    custom.AccountInfo.accountType = accountType;
                    custom.AccountInfo.rateType = rateType;
                    break;
                case "个人贷款":
                    custom = new CustomLoans();
                    custom.AccountInfo.accountType = accountType;
                    custom.AccountInfo.rateType = rateType;
                    break;
            };


            return custom;
        }

        /// <summary>
        /// 获取存款用户信息,并初始化余额
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public static Custom GetCustom(string accountNumber)
        {
            Custom custom = null;
            BankEntities c = new BankEntities();
            try
            {
                //查询对应的账户信息
                var query= from t in c.AccountInfo
                         where t.accountNo == accountNumber
                         select t;

                if (query.Count() > 0)
                {
                    //返回唯一元素，若存在多个元素，抛出异常
                    var q = query.Single();
                    //创建操作信息记录类，并初始化操作账户信息
                 
                    custom = CreateCustom(q.accountType,q.rateType);
                    custom.AccountInfo.accountNo = accountNumber;
                    custom.AccountInfo.accountName = q.accountName;
                    custom.AccountInfo.accountPass = q.accountPass;
                    custom.AccountInfo.IdCard = q.IdCard;
                }
            }
            catch
            {
                //若存在多个元素，返回null
                return null;
            }
            //查询对应账户的存取款操作信息
            var qt = from t in c.MoneyInfo
                      where t.accountNo == accountNumber
                      select t;

            //对操作金额求和，获取余额
            if (qt != null && qt.Count() > 0)
            {
                custom.AccountBalance = qt.Sum(x => x.dealMoney);
            }
            //返回设置完成的操作信息记录类
            return custom;
        }

        /// <summary>
        /// 获取指定类别的利率
        /// </summary>
        /// <param name="rateType">利率类别</param>
        /// <returns>对应类别的利率值</returns>
        public static double GetRate(RateType rateType)
        {
            string type = rateType.ToString();
            BankEntities c = new BankEntities();
            var q = (from t in c.RateInfo
                     where t.rationType == type
                     select t.rationValue).Single();
            return q.Value;
        }
        
        /// <summary>
        /// 获取该账号上一次存款的时间
        /// </summary>
        /// <param name="accountNo">账号</param>
        /// <returns></returns>
        public static DateTime getLastDepositDate(string accountNo)
        {
            DateTime lastDate = DateTime.Now;
            using(BankEntities context = new BankEntities())
            {
                
                var q = from t in context.MoneyInfo
                        where t.accountNo == accountNo
                        select t.dealDate;

                foreach(var date in q)
                {
                    lastDate = date;
                }
            }
            return lastDate;
        }

        /// <summary>
        /// 获取该账号第一次存款的时间
        /// </summary>
        /// <param name="accountNo">时间</param>
        /// <returns></returns>
        public static DateTime getFirstDepositDate(string accountNo)
        {
            DateTime firstDate = DateTime.Now;
            using(BankEntities context = new BankEntities())
            {
                var q = from t in context.MoneyInfo
                        where t.accountNo == accountNo
                        select t.dealDate;

                firstDate = q.First();
            }
            return firstDate;
        }

        /// <summary>
        /// 获取上一次存款
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public static double getLastDeposit(string accountNo)
        {
            double money = 0;
            using(BankEntities context = new BankEntities())
            {
                var q = from t in context.MoneyInfo
                        where t.accountNo == accountNo
                        select t;
                foreach(var item in q)
                {
                    money = item.dealMoney;
                }
            }
            return money;
        }
    }
}
