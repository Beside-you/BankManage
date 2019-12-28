CREATE TABLE [dbo].[Table]
(
	[名字] VARCHAR(50) NOT NULL , 
    [身份证号] VARCHAR(50) NOT NULL, 
    [贷款金额] INT NOT NULL, 
    [贷款年份] INT NOT NULL, 
    [贷款月份] INT NOT NULL, 
    [贷款日期] INT NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([名字])
)
