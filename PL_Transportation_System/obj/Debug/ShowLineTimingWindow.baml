   M S B A M L   `   `   `  ����S  OPL_Transportation_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PL_Transportation_SystemPL_Transportation_System  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   = 9BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BO;assembly=BLBO 5   	   O,clr-namespace:PL_Transportation_System.UtilsPL_Transportation_System.Utils  5	   	   L HRadialProgressbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:RadialProgressbar;assembly=RadialProgressbarRadialProgressbar 6D   3    -PL_Transportation_System.ShowLineTimingWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PL_Transportation_System     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   +Boclr-namespace:BO;assembly=BL     5   	   >utils,clr-namespace:PL_Transportation_System.Utils     5	   	   XRadialProgressbar:clr-namespace:RadialProgressbar;assembly=RadialProgressbar     6D     � Title$  Actions on line ��5   	   $	��450��6"   $��705.634��6/    � Icon$ Sources/carMain.png��5   	    � ShowInTaskbar .   � 	Resources 5      ,  &PL_Transportation_System.EnumConverter   EnumToIntConverter 	 Areas S� 
MethodName S� 
ObjectTypeL   FPL_Transportation_System.Utils.DisplayAttributeBasedObjectDataProvider S� MethodParameters M� TypeName��%�   &	        &	        5   
   6   S� 5   
   $' !GetShortListOfApplicationGestures��6+   "  6Z    5      M� 5      $ Bo:Areas��6   5      5      .��5      � $��
0,0,34,0.4q�6   .+�5   
   � $	��420��6   $��
0,0,-37,-2q�6   $	��Top=�61    � RowDefinitions 5      �� 5      $
��auto �6    $��341.6��6.   �� 5      $
��50.4 �6    ��5      �� 5      	 �� ImageSource$	 Sources/line.jpg��6   .+�5      �� $��179,11,181.6,298.4q�6   $
��Bold"�60   ���	#FF042271$��Goudy Old Style)�6Y   $*��$Choose a station to start simulation��6v   
 J� RenderTransformOrigin$
 1.821,-0.669&�6�   $��20(�6�   �� 5      -   5         ��Name$ cbLineStations��5       $ �� IsSynchronizedWithCurrentItem$
 TrueY�5       J� Opacity$	 0.7Q�5    $   $��Goudy Old Style)�62   $
��15px(�6O   ���	#FF042271$
��Bold"�6v   $
��Left=�6�   $	��Top=�6�   $	��503��6�   $��29��6�   $��
106,55,0,0q�6�   
���������6�   �� *
Stations+ �� SelectedItem 6  �� *SelectedStation+ �� Mode$ TwoWay=���5!      2  ,System.Windows.Controls.DataTemplateSelector  5"      5$      �� 5%      -   5         ��Name$ lvLineTimings��5%   H   ���	#FF042271$��Goudy Old Style)�6w   $
��Left=�6�   $	��606��6�   $
��Bold"�6�   
�������$	 0.8Q�6�   $��56,213,0,1.4q�6�   $
��15px(�6  $��2��6  ��6   �� *LineTimings+$ TwoWay=� �� View 5&      �� 5'      .P�5(      �� $��Line Number��6*    �� DisplayMemberBinding 5)      �� *
LineNumber+�� 5*      $��Last Station��6)    5+      �� *LastStation+�� 5,      $	��ETA��6)    5-      �� *ExpectedTimeTillArrive+�� 5.      $��
Trip Start��6)    5/      �� *	TripStart+52      54      �� 55      -   5         ��Name$ lblCurrentTime��55      ���	#FF042271$��Goudy Old Style)�6E   $
��Left=�6b   $��
264,92,0,0q�6}   $	��Top=�6�   $	��177��6�   $��71��6�   $��36(�6�   $
��Bold"�6�   �� 56      -   5        $��btnStopSimulation��56      $
��15px(�6.   $��Stop Simulation��6]   $��159,176,373.6,147.2q�6w   �� 57      -   5        $��btnRestartSimulation��57      $
��15px(�61   $��Restart Simulation��6c   $��375,176,157.6,147.2q�6�   58      59      5:      