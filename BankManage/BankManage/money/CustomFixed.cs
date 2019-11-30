using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BankManage.common;

namespace BankManage
{
    /// <summary>
    /// 定期存款
    /// </summary>
    public class CustomFixed : Custom
    {
        public RateType type { get; set; }
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        
        //开户，设置存款资金，
        public override void Create(string accountNumber,double money)
        {
            //不结息
            base.Create(accountNumber, money);
        }

        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType,double money)
        {
            //不可二次存款，但是结息时也是进行存款操作，因此此功能在存款界面实现
            //MessageBox.Show("仅可存款一次！");
            base.Diposit(genType, money);
            return;
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money)
        {
            //TODO:一次性取款，为用户设置应取款项
            //取款时检查时间，设置对应利率，计算利息
            //上次存款时间
            DateTime depositDate = getLastDepositDate();
            //当前时间
            DateTime now = DateTime.Now;
            //获取时间差
            TimeSpan ts = now - depositDate;
            bool beyond = false;//判断是否超出期限
            //定期一年
            if(AccountInfo.rateType.Equals(RateType.定期1年.ToString()))
            {
                if(ts.Days < 365)
                {
                    type = RateType.定期提前支取;
                }
                else if(ts.Days == 365)
                {
                    type = RateType.定期1年;
                }
                else
                {
                    type = RateType.定期1年;
                    beyond = true;
                }
            }
            //定期三年
            else if(AccountInfo.rateType.Equals(RateType.定期3年.ToString()))
            {
                if (ts.Days < 365)
                {
                    type = RateType.定期提前支取;
                }
                else if(ts.Days == 365)
                {
                    type = RateType.定期3年;
                }
                else
                {
                    type = RateType.定期3年;
                    beyond = true;
                }
            }
            //定期五年
            else if(AccountInfo.rateType.Equals(RateType.定期5年.ToString()))
            {
                if (ts.Days < 365)
                {
                    type = RateType.定期提前支取;
                }
                else if(ts.Days == 365)
                {
                    type = RateType.定期5年;
                }
                else
                {
                    type = RateType.定期5年;
                    beyond = true;
                }
            }
            //结息
            double interest = 0; ;
            if(type.Equals(RateType.定期提前支取))
            {
                //设置利息
                interest = AccountBalance * DataOperation.GetRate(type);
            }
            else if(type.Equals(RateType.定期1年))
            {
                interest = AccountBalance * DataOperation.GetRate(type);
                if(beyond)
                {
                    interest = (interest + AccountBalance) * DataOperation.GetRate(RateType.定期超期部分);
                }
            }
            else if(type.Equals(RateType.定期3年))
            {
                interest = AccountBalance * DataOperation.GetRate(type);
                if(beyond)
                {
                    interest = (interest + AccountBalance) * DataOperation.GetRate(RateType.定期超期部分);
                }
            }
            else if(type.Equals(RateType.定期5年))
            {
                interest = AccountBalance * DataOperation.GetRate(type);
                if(beyond)
                {
                    interest = (interest + AccountBalance) * DataOperation.GetRate(RateType.定期超期部分);
                }
            }
            Diposit("结息", interest);
            //超出余额，无法取款
            if (!ValidBeforeWithdraw(money)) return;
            //取款
            base.Withdraw(money);
        }
    }
}
