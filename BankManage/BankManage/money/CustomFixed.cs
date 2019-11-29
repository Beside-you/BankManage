using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankManage.common;

namespace BankManage
{
    /// <summary>
    /// 定期存款
    /// </summary>
    public class CustomFixed : Custom
    {
        //TODO:起存100

        public RateType type { get; set; }
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        
        //重写创建方法,选择对应利率
        public override void Create(string accountNumber,double money)
        {
            //TODO:选择分期
            base.Create(accountNumber, money);
            //base.Diposit("结息")
        }

        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType,double money)
        {
            //TODO:按分期结算（取款时结算？）
            //TODO:定期存款账户后期再次存款？？
            base.Diposit("存款", money);
            //结算利息
            base.Diposit("结息", DataOperation.GetRate(RateType.定期1年) * money);
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money)
        {
            //TODO:取款时检查时间，设置对应利息?
            if (!ValidBeforeWithdraw(money)) return;
            //计算利息
            double rate = DataOperation.GetRate(type) *AccountBalance;
            //添加利息
            AccountBalance += rate;
            //取款
            base.Withdraw(money);
        }
    }
}
