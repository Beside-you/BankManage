using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankManage.common;

namespace BankManage
{
    public class CustomChecking : Custom
    {
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money)
        {
            base.Create(accountNumber, money);
            base.Diposit("结息", DataOperation.GetRate(RateType.活期) * money);
        }

        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType, double money)
        {
            //TODO:起存一百
            //此处方法供开户之后的存款使用，存款时即结息
            base.Diposit("存款", money);
            //结算利息
            base.Diposit("结息", DataOperation.GetRate(RateType.活期) * money);
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money)
        {
            if (!ValidBeforeWithdraw(money)) return;
            //取款
            base.Withdraw(money);
        }
    }
}
