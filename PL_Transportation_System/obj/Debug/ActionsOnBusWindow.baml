   M S B A M L   `   `   `  ����S  OPL_Transportation_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PL_Transportation_SystemPL_Transportation_System  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   = 9BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BO;assembly=BLBO 5
   	   O,clr-namespace:PL_Transportation_System.UtilsPL_Transportation_System.Utils  5   	   1    +PL_Transportation_System.ActionsOnBusWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PL_Transportation_System     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   +Boclr-namespace:BO;assembly=BL     5
   	   >utils,clr-namespace:PL_Transportation_System.Utils     5   	     � Title$  Actions on bus��5	   	   $	��450��6    $��705.634��6-    � Icon$ Sources/carMain.png��5   D   .��5      � $��
0,0,34,0.4q�6   .+�5   
   � $	��420��6   $��
0,0,-37,-2q�6   $	��Top=�61    � RowDefinitions 5      �� 5      $
��auto �6    $��341.6��6.   �� 5      $
��50.4 �6    ��5      �� 5       �� ImageSource$ Sources/Bus.jpg��6   .+�5      �� -   5         ��Name$ lvBus��5      ���	#FF042271$��Goudy Old Style)�6j   $
��Left=�6�   $	��305��6�   $	��Top=�6�   $	��606��6�   $
��Bold"�6�   
������� J� Opacity$	 0.8Q�6�   $��	40,27,0,0q�6  $
��15px(�6  ��6&   �� *Buses+ �� Mode$ TwoWay=� �� View 5      �� 5      .P�5       �� $��License Number��6)    �� DisplayMemberBinding 5!      �� 	 �� Path$	 LicenseNumber��� 5"      $��	From Date��6)    5#      �� $	 FromDate�
 �� StringFormat$
 
dd/MM/yyyy���� 5$      $��
Total Trip��6)    5%      �� $	 	TotalTrip��� 51      $��
Is Deleted��6)    �� CellTemplate 52      �� 53   "   .R�54   &   ��  d� 	IsChecked 6/   �� *	IsDeleted+$ TwoWay=�55   #   57      58      5:      �� 5;      -   5        $
��15px(�5;      $��
Update All��6?   $��374,342,56.6,-9.4q�6T   $��2��6o   �� 5<      -   5        $
��15px(�5<      $��Add Bus��64   $��40,342,390.6,-9.4q�6F   $��2��6a   5>      5?      5@      