   M S B A M L   `   `   `  ����S  OdotNet5781_03B_7073_1160, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:dotNet5781_03B_7073_1160dotNet5781_03B_7073_1160  5   	   Y1clr-namespace:dotNet5781_03B_7073_1160.Converters#dotNet5781_03B_7073_1160.Converters  6>   L HRadialProgressbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:RadialProgressbar;assembly=RadialProgressbarRadialProgressbar 6�   )    #dotNet5781_03B_7073_1160.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:dotNet5781_03B_7073_1160     5   	   H
converters1clr-namespace:dotNet5781_03B_7073_1160.Converters     6>   Xradialprogressbar:clr-namespace:RadialProgressbar;assembly=RadialProgressbar     6�     � Title$  
Bus system��5   	   $��766.8��6   $	��778��6+    � Icon$" Resources/Images/carMain.png��67    � 	Resources 5	      @   :dotNet5781_03B_7073_1160.Converters.StatusToColorConverter   StatusToColorConverter@   :dotNet5781_03B_7073_1160.Converters.StatusToImageConverter  StatusToImageConverter��%:   &	        &	        5
   
   6,     5   
   6,   5      .��5      � $��	0,0,3.4,4q�6   ��5   
   �� 5       �� ImageSource$( "Resources/Images/BusBackground.jpg��6   .+�5   
   �� $��262,15,236.8,628.8q�6   $��50(�6,   $��Goudy Old Style)�6;   $
��Bold"�6X   .��6j   
Bus System6v   �� 5   
   -   5        $��Stretch=�5      $��Stretch=�6v   $	��401��6�   $	��Top=�6�   $��60,104,59.8,0q�6�   ���	#FFB64848
�������$��Goudy Old Style)�6.  $
��20px(�6K  ��68   �� *	BusList+ �� UpdateSourceTrigger$ Explicit=� �� View 5      �� 5      P�5      �� 5      $��Status��6)    �� CellTemplate 5      �� 5   "   .R�5   &   �  � RowDefinitions 5   *   �� 5   .   $��65 �6<    � ColumnDefinitions 5   *   �� 5   .   $��65 �6?   �� 5   .   $
��auto �6?   .+�5!   *   )  #RadialProgressbar.RadialProgressBar  $��3q�5"   -   
	   Min$	 0Q�5$   -   

   Max$	
 100Q�5%   -      
DigitStyle$  Percentage_1_DecimalPlaces=�5&   -      BackArcBrush �#444444   Glow .     Duration 5#   -   �� *DurationOfUnavailability+$ PropertyChanged=� �� Mode$ TwoWay=�   ValueArcBrush 5(   -   �� *Status+ �� 	Converter# [     NumericBrush 5)   -   �� *Status+# [  5*   -   �� 5+   *   $��1��60   $��63��6@   $��63��6L   ��6W   �� *Status+# [  � ToolTip 5,   .   _� 5-   2   ��6:   ��  �� Path$ Status�5/   +   50   '   51   #   53      �� 54      $��Travel��6)    55      �� 56   "   .R�57   &   �� -   5        $��Travel��57   -   $��0,0,20,0q�6>   $��80��6P   ���	#FF2DCD3B6�   58   #   5:      �� 5;      $��	Treatment��6)    5<      �� 5=   "   .R�5>   &   �� -   5        $��Treat��5>   -   $��0,0,20,0q�6=   $��80��6O   ���	#FF61CFDF6q   5?   #   5A      �� 5B      $
��Fuel��6)    5C      �� 5D   "   .R�5E   &   �� -   5        $
��Fuel��5E   -   $��0,0,20,0q�6<   $��80��6N   ���	#FFE9EA7A6o   5F   #   5H      �� 5I      $��License Number��6)    5J      �� 5K   "   .R�5L   &   �� ��6,   �� *LicenseNumber+ 6N   �� 5M   #   5O      5Q      5S      �� 5T   
   -   5        $��236,543,236.8,100.8q�5T   :   $��Add Bus��6W   $��30(�6i   
���   ����	#FFA8AE20$	��2pxq�6�    J� RenderTransformOrigin$ 20,20&�6�   
������� J� Opacity$	 0.7Q�6�   $��Goudy Old Style)�6�   $
��Bold"�6  5U      5V      