using BankManage.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankManage.money
{
    class CustomWhole : Custom
    {
        
        public RateType type { get; set; }
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">账号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money)
        {
            //不结息
            base.Create(accountNumber, money);
        }

        /// <summary>
        /// 存款
        /// </summary>
        /// <param name="genTyep"></param>
        /// <param name="money"></param>
        public override void Diposit(string genType, double money)
        {
            //每次存款固定金额，检测存款日期间隔是否合法
           
            // DateTime lastDepositDate = getLastDepositDate();
            DateTime lastDepositDate = DataOperation.getLastDepositDate(AccountInfo.accountNo);

            //MessageBox.Show(lastDepositDate.ToString());

            DateTime now = DateTime.Now;
            //TimeSpan ts = now - lastDepositDate;
            //TODO:超出指定期限的违规存款是否判断为违规
            //存款违规
            double lastDeposit = DataOperation.getLastDeposit(AccountInfo.accountNo);
            if((now.Year == lastDepositDate.Year && now.Month > lastDepositDate.Month + 1) || (now.Year - 1 == lastDepositDate.Year && now.Month == 1 && lastDepositDate.Month == 12) || money != lastDeposit)
            {
                //超出存款期限,修改账户利率类型为违规利率
                type = RateType.零存整取违规;
                using(BankEntities context = new BankEntities())
                {
                    var q = from t in context.AccountInfo
                            where t.accountNo == AccountInfo.accountNo
                            select t;
                    foreach(var item in q)
                    {
                        item.accountType = type.ToString();
                    }
                    context.SaveChanges();
                }
            }
            base.Diposit(genType, money);
        }

        public override void Withdraw(double money)
        {
            //取款
            //判断是否超期
            bool beyond = false;
            if (AccountInfo.rateType != RateType.零存整取违规.ToString())
            {
                DateTime firstDate = DataOperation.getFirstDepositDate(AccountInfo.accountNo);
                DateTime now = DateTime.Now;
                TimeSpan ts = now - firstDate;

                //一年
                if (AccountInfo.rateType.Equals(RateType.零存整取1年.ToString()))
                {
                    if (ts.Days < 365)
                    {
                        type = RateType.零存整取违规;
                    }
                    else if (ts.Days == 365)
                    {
                        type = RateType.零存整取1年;
                    }
                    else
                    {
                        type = RateType.零存整取1年;
                        beyond = true;
                    }
                }
                //三年
                else if (AccountInfo.rateType.Equals(RateType.零存整取3年.ToString()))
                {
                    if (ts.Days < 365 * 3)
                    {
                        type = RateType.零存整取3年;
                    }
                    else if (ts.Days == 365 * 3)
                    {
                        type = RateType.零存整取3年;
                    }
                    else
                    {
                        type = RateType.零存整取3年;
                        beyond = true;
                    }
                }
                //五年
                else if (AccountInfo.rateType.Equals(RateType.零存整取5年.ToString()))
                {
                    if (ts.Days < 365 * 3)
                    {
                        type = RateType.零存整取5年;
                    }
                    else if (ts.Days == 365 * 3)
                    {
                        type = RateType.零存整取5年;
                    }
                    else
                    {
                        type = RateType.零存整取5年;
                        beyond = true;
                    }
                }

                //结息
                double interest = 0;
                if (type.Equals(RateType.零存整取违规))
                {
                    interest = AccountBalance * DataOperation.GetRate(type);
                }
                else
                {
                    interest = AccountBalance * DataOperation.GetRate(type);
                    if(beyond)
                    {
                        interest = (AccountBalance + interest) * DataOperation.GetRate(type);
                    }
                }

                Diposit("结息", interest);
                //取款，此处放在最后是因为取款时结息，因此要结息之后才判断是否超出
                //同时也导致一个问题就是无论取款是否成功，结息都会完成
                double all = DataOperation.GetCustom(AccountInfo.accountNo).AccountBalance;
                base.Withdraw(all);
                

            }
        }
    }
}
