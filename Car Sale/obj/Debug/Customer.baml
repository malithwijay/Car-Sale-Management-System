   M S B A M L   `   `   `  ????C  ?Car Sale, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Car_SaleCar_Sale  5          Car_Sale.Customer   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Car_Sale     5      -   5        .??5      ? 
???????  ? ColumnDefinitions  5   
   ?? 5      $??60 ?6   ?? 5      ?? 5      $	??100 ?6    ? RowDefinitions 5   
   ?? 5      $??20 ?6   ?? 5      $??80 ?6   ?? 5      ?? 5      $	??120 ?6   ?? 5      $??20 ?6   .+?5   
   ?? $??1??6   $??1??6"   $
??Left=?62   $??Center=?6M    ?? Orientation$ 
Horizontal=?6h   .+?5      ?? $??Logo\carLogo.png??6   $??90??6.   $??80??69   $
??Left=?6E   $??80,0,0,0q?6`   ?? 5      $??Our Customers...??6   $??25(?60   $??	Gill Sans)?6>   $
??Bold"?6U   $??Center=?6g   5      ?? 5    
   $y?Hidden=?6   .+?5!      ?? -   5         ??Name$ 	ed_cus_id??5!      ?? 5"      -   5        $ ed_cus_fname??5"      ?? 5#      -   5        $ ed_cus_lname??5#      ?? 5$      -   5        $ ed_cus_gender??5$      ?? 5%      -   5        $ ed_cus_telephone??5%      ?? 5&      -   5        $ ed_cus_email??5&      ?? 5'      -   5        $ ed_cus_addressno??5'      ?? 5(      -	   5        $ ed_cus_addline1??5(      ?? 5)      -
   5        $ ed_cus_addline2??5)      ?? 5*      -   5        $ ed_cus_city??5*      5+      ?? 5-   
   $??2??6   $??1??6"   $??5,20,0,0q?64   $
??Left=?6F   .+?5/      &   System.Windows.Controls.DataGrid  -   5          Name$ Customer_view??5/   1   $
??1200??6   $	??375??6$      AutoGenerateColumns .     CanUserResizeColumns .  $ ?? IsSynchronizedWithCurrentItem$
 TrueY?50   a      SelectionUnit$ FullRow=?51      
???????
???   ???50      ?? 	 ?? Path$	 LoadDataBinding?
   Columns
 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ Customer NIC??6N      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6d      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ?? $	 CustomerNIC?6p     55      $ 
First Name??6L   $	 150 6`    .   6)   ?? $	 	FirstName?6l     56      $ 	Last Name??6K   $	 150 6^    .   6)   ?? $	 LastName?6j     57      $ Gender??6I   $	 150 6Y    .   6)   ?? $	 Gender?6e     58      $ 	Telephone??6L   $	 150 6_    .   6)   ?? $	 	Telephone?6k     59      $ Email??6H   $	 150 6W    .   6)   ?? $	 Email?6c     5:      $ 
Address No??6L   $	 150 6`    .   6)   ?? $	 	AddressNO?6l     5;      $ Address Line 1??6S   $	 150 6k    .   6)   ?? $	 FirstAddressLine?6w     5<      $ Address Line 2??6T   $	 150 6l    .   6)   ?? $	 SecondAddressLine?6x     5=      $
 City??6G   $	 150 6U    .   6)   ?? $
	 City?6a   4  .System.Windows.Controls.DataGridTemplateColumn  5>         CellTemplate 5?      ?? 5@      .R?5A   "   ??  ??Name$ 
update_btn??6Q   -   5        $??Update??5A   )   $??25??6:   $??80??6F   6e   5B      5D      5G      5H      ?? 5N   
   -   5         ??Name$ instruction_txt??5N   v   $??3??6   $??1??6"   $ Vertical=?62   $
??Left=?6I   $??10,8,0,0q?6d   $y?Hidden=?6?   .+?5O      ?? $"??Instructions to Update Data,??6   $
??Bold"?6<   
???  ??6N   ?? 5P      $E???*Select Cell/Cells you wants to edit and change values of them.??6   ?? 5Q      $,??&*Then Press Enter to chage cell value.??6   ?? 5R      $;??5*Select the row again (the row that you want to edit)??6   ?? 5S      $=??7*Check text boxs to double check the values are changed??6   ?? 5T      $??*Press Update to Update??6   5U      ?? 5W   
   $??3??5X      $??2??5Y      $
??Left=?5Z      $??Center=?5[      $ Vertical=?5\      $??
-50,0,0,10q?5]      .+?5_      ?? -   5        $ btn_Add??5_      $	??Add??6&   
???????$??10q?6[   ??5a      ?? 5b       ?? 
TargetType" ??6   .?5c      ??  ?? Property ? ?? ?? Value
 ??? ?67   ?? 5d       ?@?Cursor$
 Hand??63    ?? Triggers 5e      P? 5f       P? Property ?@J?IsMouseOver P? Value . . ?5g   "   ??  ? ??
 ??ح?6?   5h      5j      ??5l      ?? 5m       ?? 
TargetType" ??6&   .R?5n      ?? $	??100??6!   $??25??6-    ?? CornerRadius$ 12??69   #??z??6K   ._?5o      ?? $??Center=?6/   $??Center=?6L   5p      5q      5s      5u      ?? 5v   
   $??3??5x      $??1??5y      $??10,8,0,0q?5z      $
??Left=?5{      $ Vertical=?5|      $y?Visible=?5}      .+?5~      ?? $
??Bold"?5      
???  ??$"??Instructions to Update Data,??5?      ?? 5?      $E???*Select Cell/Cells you wants to edit and change values of them.??6   ?? 5?      $-??'*Then Press Enter to change cell value.??6   ?? 5?      $;??5*Select the row again (the row that you want to edit)??6   ?? 5?      $=??7*Check text boxs to double check the values are changed??6   ?? 5?      $??*Press Update to Update??6   5?      5?      5?      