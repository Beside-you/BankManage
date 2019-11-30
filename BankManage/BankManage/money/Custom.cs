using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BankManage;

namespace BankManage
{
    /// <summary>
    /// 保存客户发生的最新业务信息
    /// </summary>
    public class Custom
    {
        /// <summary>
        /// 存款客户的帐户基本信息
        /// </summary>
        public AccountInfo AccountInfo { get; set; }
        /// <summary>
        /// 存款发生信息
        /// </summary>
        public MoneyInfo MoneyInfo { get; set; }
        /// <summary>
        /// 帐户余额
        /// </summary>
        public double AccountBalance { get; set; }
        public Custom()
        {
            AccountInfo = new AccountInfo();
            MoneyInfo = new MoneyInfo();
        }

        BankEntities context = new BankEntities();
        /// <summary>
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public virtual void Create(string accountNumber, double money)
        {
            //设置用户开户资金
            this.AccountBalance = money;
            bool success = false;
            //开户信息插入到数据库
             try
             {
                //将用户信息插入到实体数据模型中
                  context.AccountInfo.Add(AccountInfo);
                  context.SaveChanges();
                  success = true;
              }
             catch(Exception err)
             {
                success = false;
                MessageBox.Show("开户失败！"+err.Message);
             }
            //若数据库更新成功，
            if (success)
            {
                //设置用户存款存取款操作的操作账号
                this.MoneyInfo.accountNo = accountNumber;
                //添加操作记录
                InsertData("开户", money);
            }
        }

        /// <summary>
        ///存款 
        /// </summary>
        /// <param name="genType">类型</param>
        /// <param name="money">金额</param>
        public virtual void Diposit(string genType, double money)
        {
            if (money <= 0)
            {
                throw new Exception("存款金额不能为零或负值");
            }
            else
            {
                //修改余额
                AccountBalance += money;
                //添加存取款操作记录
                InsertData(genType, money);
            }
        }

        /// <summary>
        ///检查是否允许取款，允许返回true，否则返回false
        /// </summary>
        /// <param name="money">取款金额</param>
        public bool ValidBeforeWithdraw(double money)
        {
            if (money <= 0)
            {
                MessageBox.Show("取款金额不能为零或负值");
                return false;
            }
            //TODO:考虑利息
            if (money > AccountBalance)
            {
                MessageBox.Show("取款数不能比余额大");
                return false;
            }
            return true;
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public virtual void Withdraw(double money)
        {
            AccountBalance -= money;
            //添加取款记录
            InsertData("取款", -money);
        }

        /// <summary>
        /// 在MoneyInfo表中添加新记录
        /// </summary>
        /// <param name="genType">发生类别</param>
        /// <param name="money">发生金额</param>
        public void InsertData(string genType, double money)
        {

            //初始化存取款操作信息
            MoneyInfo.accountNo = this.AccountInfo.accountNo;//用户信息表
            MoneyInfo.dealDate = DateTime.Now;//操作发生日期
            MoneyInfo.dealType = genType;//操作类型
            MoneyInfo.dealMoney = money;//操作金额
            MoneyInfo.balance = AccountBalance;//账户余额
            try
            {
                //将信息插入表中
                context.MoneyInfo.Add(MoneyInfo);
                context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加交易记录失败：" );
            }
           
        }

        /// <summary>
        /// 获取上一次的存款日期
        /// </summary>
        /// <returns>上一次存款日期</returns>
        public virtual DateTime getLastDepositDate()
        {
            return MoneyInfo.dealDate;
        }
    }
}
