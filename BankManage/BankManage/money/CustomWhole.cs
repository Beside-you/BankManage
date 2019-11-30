using BankManage.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //TODO:起存五元，检测存款日期是否超出期限
            base.Diposit(genType, money);
        }

        public override void Withdraw(double money)
        {
            
        }
    }
}
