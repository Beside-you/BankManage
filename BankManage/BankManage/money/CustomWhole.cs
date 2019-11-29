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
        //TODO:起存100
        public RateType type { get; set; }
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">账号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money)
        {
            //TODO:利率选择，默认一年？定期存款同样问题！
            base.Create(accountNumber, money);
            
        }

        /// <summary>
        /// 存款
        /// </summary>
        /// <param name="genTyep"></param>
        /// <param name="money"></param>
        public override void Diposit(string genTyep, double money)
        {
            base.Diposit("存款", money);
            base.Diposit("结息", DataOperation.GetRate(RateType.零存整取1年) * money);
        }
    }
}
