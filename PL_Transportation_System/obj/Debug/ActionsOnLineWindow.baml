   M S B A M L   `   `   `  ����S  OPL_Transportation_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PL_Transportation_SystemPL_Transportation_System  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   = 9BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BO;assembly=BLBO 5
   	   O,clr-namespace:PL_Transportation_System.UtilsPL_Transportation_System.Utils  5   	   2    ,PL_Transportation_System.ActionsOnLineWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PL_Transportation_System     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   +Boclr-namespace:BO;assembly=BL     5
   	   >utils,clr-namespace:PL_Transportation_System.Utils     5   	     � Title$  Actions on line ��5	   	   $	��450��6"   $��705.634��6/    � Icon$ Sources/carMain.png��5   D    � ShowInTaskbar .   � 	Resources 5      ,  &PL_Transportation_System.EnumConverter   EnumToIntConverter 	 Areas S� 
MethodName S� 
ObjectTypeL   FPL_Transportation_System.Utils.DisplayAttributeBasedObjectDataProvider S� MethodParameters M� TypeName��%�   &	        &	        5   
   6   S� 5   
   $' !GetShortListOfApplicationGestures��6+   "  6Z    5      M� 5      $ Bo:Areas��6   5      5      .��5      � $��
0,0,34,0.4q�6   .+�5   
   � $	��420��6   $��
0,0,-37,-2q�6   $	��Top=�61    � RowDefinitions 5      �� 5      $
��auto �6    $��341.6��6.   �� 5      $
��50.4 �6    ��5      �� 5      	 �� ImageSource$	 Sources/line.jpg��6   .+�5      �� -   5        
 ��Name$
 lvLine��5   B   ���	#FF042271$��Goudy Old Style)�6j   $
��Left=�6�   $	��305��6�   $	��Top=�6�   $	��606��6�   $
��Bold"�6�   
������� J� Opacity$	 0.8Q�6�   $��	40,27,0,0q�6  $
��15px(�6  ��6   �� *Lines+ �� Mode$ TwoWay=� �� View 5      �� 5      .P�5       �� $��Line Number��6)    �� DisplayMemberBinding 5!      ��  �� Path$ 
LineNumber��� 5"      $
��Area��6)    �� CellTemplate 5#      �� 5$   "   .R�5%   &   �� $	��200��6�    � DataContext 6/   �� ��6G   ��  �� Source# [  �� SelectedIndex 6~   �� *Area+ �� 	Converter# [  $ TwoWay=�6�   5&   #   5(      �� 5)      $��
Is Deleted��6)    5*      �� 5+   "   .R�5,   &   ��  d� 	IsChecked 6/   �� *	IsDeleted+$ TwoWay=�5-   #   5/      �� 50      $��Update��6)    51      �� 52   "   .R�53   &   �� -   5        $ 1Q�53   -   $
��15px(�69   $��0,0,20,0q�6I   $��40��6s   $��40��6    6[   �� ��54   *   �� 55   .   $	 Sources/update.jpg��69   57   '   58   #   5:      5;      5=      �� 5>      -   5        $
��Left=�5>      $
��15px(�60   $��Add Line��6P   $��110,342,0,-14.4q�6c   $	��181��6|   $��2��6�   �� 5?      -   5        $
��15px(�5?      $��
Update All��6?   $��420,342,87.6,-14.4q�6T   $��2��6p   5@      5A      5B      