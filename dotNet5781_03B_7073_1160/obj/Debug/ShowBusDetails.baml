   M S B A M L   `   `   `  ����S  OdotNet5781_03B_7073_1160, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:dotNet5781_03B_7073_1160dotNet5781_03B_7073_1160  5   	   -    'dotNet5781_03B_7073_1160.ShowBusDetails   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:dotNet5781_03B_7073_1160     5   	     � Title$  Bus Details��5   	   $��499.2��6   $��480.8��6,   $��Goudy Old Style)�6:   $��19(�6W    � Icon$" Resources/Images/carMain.png��6e   .��5	      �  � RowDefinitions 5
   
   �� 5      $��3* �6   �� 5      $	��44* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF56DBDD��6   � 5      $ White��6    � Offset$ 0.641Q�6-   � 5      $ 	#FF35BF32��6   $ 1Q�61   � 5      $ 	#FFBBE9BA��6   $ 0.854Q�61   5      .+�5   
   �� $��50,0,-0.6,-0.4q�6   $��2��6-   .+�5      �� $��Bus Details��6   $
��Bold"�6*   $��112,20,171.8,20q�6<   ���	#FF1040086U   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��LicenseNumber:��6   $
��Bold"�61   �� 5      ��6   �� *SelectedBus.LicenseNumber+5      �� 5       $ 
Horizontal=�6   .+�5"      �� $��Kilometrage:��6   $
��Bold"�6/   �� 5#      ��6   �� *SelectedBus.Kilometrage+5$      �� 5%      $ 
Horizontal=�6   .+�5'      �� $��BusStartDate:��6   $
��Bold"�60   �� 5(      ��6   �� *SelectedBus.BusStartDate+ �� StringFormat$ 
dd/MM/yyyy��5)      �� 5*      $ 
Horizontal=�6   .+�5,      �� $��Fuel:��6   $
��Bold"�6(   �� 5-      ��6   �� *SelectedBus.Fuel+5.      �� 5/      $ 
Horizontal=�6   $��	0,0,0.4,0q�62   .+�51      �� $��
Treatment:��6   $
��Bold"�6-   �� 52      ��6   �� *SelectedBus.Treatment+53      �� 54      $ 
Horizontal=�6   .+�56      �� $��LastTreatmentDate:��6   $
��Bold"�65   �� 57      ��6   �� *SelectedBus.LastTreatmentDate+58      �� 59      $ 
Horizontal=�6   .+�5;      �� $��Status:��6   $
��Bold"�6*   �� 5<      ��6   �� *SelectedBus.Status+5=      5@      � 5B   
   $��0,286,9.4,-0.4q�6   $��1��6'   .+�5C      �� -   5        $��44,35,248.8,68.8q�5C      $��	Treat Car��6C   $��20(�6W   
���   ����	#FFA8AE20$	��2pxq�6�   
�������	 J� Opacity$		 0.5Q�6�   $��Goudy Old Style)�6�   $
��Bold"�6�   �� 5D      -   5        $��234,35,40.8,68.8q�5D      $��Fuel Car��6B   $��20(�6U   
���   ����	#FFA8AE20$	��2pxq�6�   
�������$		 0.5Q�6�   $��Goudy Old Style)�6�   $
��Bold"�6�   5E      5F      5G      