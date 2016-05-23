# SQL-Insert-Generator
Convert tab-delimited data into SQL INSERT statements

## Acquiring tab-delimited data
 - Right-click a query result set in SQL Server Management Studio and select "Save Results As...". Save as a tab-delimited text file.

## Usage
 - Clone the repository
 - Build using Visual Studio
 - Run SqlInsertGenerator.exe
 - Add a file name, database name, and table name
 - Add column headings
 - A file called "output.sql" will be generated
 
## Example input
*data.txt:*
```
1625	FullName	Robin Herd
1626	Industry	Software Development
1627	Role	Software Developer
```

## Example usage
```
C:\Users\robin.herd\Desktop\Repos\SqlInsertGenerator\SqlInsertGenerator\bin\Debu
g>SqlInsertGenerator

************************************
Welcome to the SQL INSERT generator.
************************************

Tab-delimited file name: data.txt
Database name: Robin
Table name: CustomFields

******************************************************************
Column headings. Enter '#' for any columns that should be ignored.
******************************************************************

Column 0 heading (example value '1625'): #
Column 1 heading (example value 'FullName'): FieldName
Column 2 heading (example value 'Robin Herd'): FieldValue
```

## Example output
*output.sql:*
```
INSERT INTO [Robin].[dbo].[CustomFields] ([FieldName], [FieldValue]) VALUES ('FullName', 'Robin Herd')
INSERT INTO [Robin].[dbo].[CustomFields] ([FieldName], [FieldValue]) VALUES ('Industry', 'Software Development')
INSERT INTO [Robin].[dbo].[CustomFields] ([FieldName], [FieldValue]) VALUES ('Role', 'Software Developer')
```
