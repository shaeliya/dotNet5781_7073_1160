   M S B A M L   `   `   `  ����S  OPL_Transportation_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PL_Transportation_SystemPL_Transportation_System  5   	   L HRadialProgressbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:RadialProgressbar;assembly=RadialProgressbarRadialProgressbar 5   	   %    PL_Transportation_System.AddBus   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PL_Transportation_System     5   	   XRadialProgressbar:clr-namespace:RadialProgressbar;assembly=RadialProgressbar     5   	     � Title$  Add Bus��5	   	   $��499.2��6   $��480.8��6(    � Icon$ Sources/carMain.png��66   $��Times New Roman)�6Q   $��19(�6n   ��5
      �� 5   
    �� ImageSource$ Sources/bus.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��237* �6   �� 5      $
��238* �6    � RowDefinitions 5   
   �� 5      $	��55* �6   �� 5      $
��414* �6   .+�5   
   ��  J� Opacity$
 0.60Q�6   $
��Left=�6!   $	��409��6<   $��	13,10,0,0q�6I   $	��Top=�6\   $	��430��6t   $��2��6�   $��2��6�   �� 5   
   $��Add Bus��6   $��Goudy Old Style)�6#   $
��25px(�6@   ���	#FF042271$
��Bold"�6g   $��2��6y   $��168,10,129.4,10.2q�6�   � 5   
   $��0,286.8,9.4,-9.4q�6   $��1��6)   $��2��66   .+�5      �� -   5        $	��Add��5   V   $��15(�6d   $��Goudy Old Style)�6r   ���	#FF042271$	��2pxq�6�   
�������$	 0.5Q�6�   $
��Bold"�6�   $��65,-3,282,97q�6�   ��6   �� *CancelAddBusCommand+6�   �� 5      -   5        $��Cancel��5   Y   $��15(�6j   $��Goudy Old Style)�6x   ���	#FF042271$	��2pxq�6�   
�������$	 0.5Q�6�   $
��Bold"�6�   $��281,-3,53,97q�6�   ��6   �� *CancelAddBusCommand+6�   5      �� 5   
   -   5         ��Name$ tbLicenseNumber��5      ���	#FFFEFFFF$��222,13.8,142.4,373.6q�6C   $��2��6a   $��1��6u   �� 5   
   $��Goudy Old Style)�6   ���	#FF042271$
��15px(�6E   $
��Bold"�6V   $��License Number:��6h   $��58,9.8,9.8,361.6q�6�   $��1��6�   (  "System.Windows.Controls.DatePicker  5    
   -   5          Name$ 
tbFromDate��5       ���	#FF042271$��15(�6@   $��222,52.8,142.4,329.6q�6N   $��1��6l   $��2��6y   �� 5!   
   $��Goudy Old Style)�6    $
��15px(�6=   ���	#FF042271$
��Bold"�6d   $��
From Date:��6v   $��58,52.8,75.8,320.6q�6�   $��1��6�   �� 5"   
   -   5        $ tbTotalTrip��5"      ���	#FFFEFFFF$��222,101.8,142.4,285.6q�6?   $��1��6^   $��2��6k   �� 5#   
   $��Goudy Old Style)�6    $
��15px(�6=   ���	#FF042271$
��Bold"�6d   $��Total Trip:��6v   $��58,98.8,75.8,278.6q�6�   $��1��6�   �� 5$   
   -   5        $ tbFuelRemain��5$      ���	#FFFEFFFF$��222,145.8,142.4,241.6q�6@   $��1��6_   $��2��6l   �� 5%   
   $��Goudy Old Style)�6    $
��15px(�6=   ���	#FF042271$
��Bold"�6d   $��Fuel Remain:��6v   $��58,140.8,46.8,237.6q�6�   $��1��6�   �� 5&   
   -   5        $ tbTreatment��5&      ���	#FFFEFFFF$��222,185.8,142.4,201.6q�6?   $��1��6^   $��2��6k   �� 5'   
   $��Goudy Old Style)�6    $
��15px(�6=   ���	#FF042271$
��Bold"�6d   $��
Treatment:��6v   $��58,181.8,61.8,194.6q�6�   $��1��6�     5(   
   -   5        $ dpLastTreatmentDate��5(      ���	#00000000���	#FF042271$��15(�6`    J� OpacityMask �	#FFE46E6E$��2��6�   $��222,227.8,142.4,150.6q�6�   $��1��6�   	 J� RenderTransformOrigin$	 0.428,-0.021&�6�   ��5)      �� 5*      
 �� EndPoint$
 0.5,0&�6&    �� 
StartPoint$ 0.5,1&�67   .��5+      �  � Color$ 	#FFA3AEB9��6#    � Offset$ 0Q�65   � 5,      $ 	#FF8399A9��6#   $ 0.375Q�65   � 5-      $ 	#FF718597��6#   $ 0.375Q�65   � 5.      $ 	#FF131618��6#   $ 1Q�65   5/      51      �� 52   
   $��Goudy Old Style)�6    $
��15px(�6=   ���	#FF042271$
��Bold"�6d   $��LastTreatmentDate:��6v   $��58,224.8,9.8,150.6q�6�   $��1��6�   54      55      