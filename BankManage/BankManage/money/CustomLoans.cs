using BankManage.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankManage.money
{
    class CustomLoans : Custom
    {
        public RateType type { get; set; }

        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">账号</param>
        /// <param name="money">贷款金额</param>
        public override void Create(string accountNumber, double money)
        {
            //贷款操作，金额为负
            //开户即结息，计算最终还款金额
            switch (AccountInfo.rateType)
            {
                case "个人贷款1年":
                    money = money * DataOperation.GetRate(RateType.个人贷款1年) + money;
                    break;
                case "个人贷款3年":
                    money = money * DataOperation.GetRate(RateType.个人贷款3年) + money;
                    break;
                case "个人贷款5年":
                    money = money * DataOperation.GetRate(RateType.个人贷款5年) + money;
                    break;
            }
            base.Create(accountNumber, money * -1);
        }

        public override void Diposit(string genType, double money)
        {

            //每次还款固定金额，检查时间
            DateTime lastDepositDate = DataOperation.getLastDepositDate(AccountInfo.accountNo);

            DateTime now = DateTime.Now;

            //超出规定期限未还款，罚款
            if ((now.Year == lastDepositDate.Year && now.Month > lastDepositDate.Month + 1) || (now.Year - 1 == lastDepositDate.Year && now.Month == 1 && lastDepositDate.Month == 12))
            {
                using (BankEntities context = new BankEntities())
                {
                    //罚款贷款额度的20%
                    var q = from t in context.MoneyInfo
                            where t.accountNo == AccountInfo.accountNo && t.dealType == "开户"
                            select t;

                    base.Diposit("罚款", q.First().dealMoney * 0.2);
                }
            }

            base.Diposit(genType, money);

        }

        public override void Withdraw(double money)
        {
            MessageBox.Show("贷款账户，禁止取款");
        }
    }
}
