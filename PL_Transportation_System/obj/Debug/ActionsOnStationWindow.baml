   M S B A M L   `   `   `  ����S  OPL_Transportation_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PL_Transportation_SystemPL_Transportation_System  5   	   5    /PL_Transportation_System.ActionsOnStationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PL_Transportation_System     5   	     � Title$  Actions on station��5   	   $	��450��6$   $	��800��61    � Icon$ Sources/carMain.png��6=   .��5	      � ��5
   
   �� 5       �� ImageSource$ Sources/station.jpg��6   .+�5   
   �� -   5         ��Name$ 	lvStation��5      $	��315��6&   ���	#FF042271$
��Bold"�6J   $��Goudy Old Style)�6]   $
��15px(�6z   $	��Top=�6�   $��33,10,27.6,0q�6�    J� Opacity$	 0.8Q�6�   ��6�   �� *
Stations+ �� View 5      �� 5      .P�5      �� $�� Name��6%    �� DisplayMemberBinding 5      ��  �� Path$
 Name��� 5      $��
 Longitude��6%    5      �� $ 	Longitude��� 5      $��	 Latitude��6%    5      �� $ Latitude��� 5      $�� Adress��6%    5      �� $ Adress��� 5      $��
Is Deleted��6%    �� CellTemplate 5      �� 5      .R�5   "   �� 	 d� 	IsChecked	 6+   �� *	IsDeleted+
 �� Mode$
 TwoWay=�5      5      �� 5      $��View Lines of Station��6%    5       �� 5!      .R�5"   "   �� -   5        $��40��5"   )   $��40��65   $��0,0,20,0q�6A    � DataContext 6S   �� ��5#   &   �� 5$   *   $ Sources/view.jpg��65   5&   #   5'      5)      5*      5,      �� 5.   
   -   5        $
��Left=�5.      $
��15px(�6,   $��Add Station��6O   $��145,342,0,10q�6e   $	��181��6|   �� 5/   
   -   5        $��Right=�5/      $
��15px(�6.   $��
Update All��6W   $��0,342,138.6,10q�6l   $	��181��6�   50      51      