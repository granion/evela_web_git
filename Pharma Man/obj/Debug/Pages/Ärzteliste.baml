   M S B A M L   `   `   `  ����d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  5      E APharma Man, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Pharma_Man.UserControlsPharma_Man.UserControls 5      3clr-namespace:Pharma_Man.PagesPharma_Man.Pages 5      "   Pharma_Man.Pages.Ärzteliste   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      cbingRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5      >UserControls%clr-namespace:Pharma_Man.UserControls     5      0localclr-namespace:Pharma_Man.Pages     5        N� Title$  	Tagesplan��5       � 	Resources 5       �� Source�� 5   
   $ ../Resources.xaml?�6   %    .��5      � -   5         �Name$ 	Grid_Page��5      .+�5   
   � -   5        $ 
Grid_Modal��5       K� ZIndex$ 2��6#   $y�Hidden=�64   .+�5      � $ 3��6    � RowDefinitions 5      �� 5      $��* �6$   �� 5      $
��auto �6$   �� 5      $��* �6$    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5       $��2* �6'   �� 5!      $��* �6'   .+�5$      �� -   5        $��3��5$      $��3��6-   $ 3��6>   ��5%      �� 5&      .R�5'      �� 
���   �$��0��6H    J� Opacity$
 0.85Q�6\   ��66   Z� 6\   5(      5*      � 5.      $��1��6   $��1��6'   $ 3��64   .+�5/      �� $��5q�6   $��1q�50      
������� J� Effect 51      3  -System.Windows.Media.Effects.DropShadowEffect  52      	   ShadowDepth$	 1Q�53   !   
   Opacity$
 1Q�54   !      
BlurRadius$ 50Q�55   !      	Direction$ 0Q�56   !      Color$ White��57   !   59      � 5:      -   5        $ Grid_ModalContent��5:      ���	#F2191919$��6q�6M    5<      �� 5=      $
��auto �6,   �� 5>      $��* �6,   $	��100��67   �� 5?      $��50 �6,   .+�5B      � $��0��6   .+�5C      �� $��Priorisieren��6$   
�������$��18(�6N   6^   �� 5D      -   5         ��Name$ lbl_Arzt_Name��5D   $   $��DR. TEST��6;   ���	#FF5D5D5D$��14(�6e   $��99,2,0,0q�6s   $��Center=�6�   $
��Left=�6�   6�   �� 5E      -   5        $��Right=�5E   %   $��50��6A      
FlatButton#��[  6^   .��6�   X6�   5F      � 5H      $��1��6   .+�5I      �� ���	#FF232323$	��0,1q�6=   �� 5J       �� Orientation$ Vertical=�6)   $��20q�6@   $��Center=�6L   .+�5K   "   �� -   5        $ label��5K   (   $��
Priorität��67   $
��Left=�6K   $	��Top=�6h   
�������$��0q�6�   �� 5L   "   -   5         ��Name$ tb_Prio��5L   *   $
��Left=�6;   $��23��6V    �� TextWrapping$
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   $	��120��6�   $��4,0,0,0q�6�   5M      5N      �� 5P      -	   5        $��2��5P   !   $��
Auswählen��6.   #��[  6B   $��Right=�6f   $	��100��6�   $��
0,10,40,10q�6�   6�   5S      5T      5U      5V      � 5Y   
   -
   5        $ Grid_Content��5Y      $��Stretch=�6%   $��Stretch=�6C   $��
10,0,10,10q�6_    5Z      �� 5[      $	��100 �6    �� 5\      $��* �6    .+�5_      �� -   5        $ lbl_pageCaption��5_   !   $��0��6   $��Ärzteliste��6:   $��10,0,0,0q�6O     Label_PageCaption#��[ 6a   � 5a      $��1��6   .+�5b      �� -   5        $��ärzteListe��5b   (   $��1��6   $ 4��6:   ���	#FF252525$��0q�6b   
������� �� View 5c      �� 5d       �� AllowsColumnReorder .  .P�5e      ��  �� Width$	 120��6-    �� DisplayMemberBinding 69   �� *ID+��5f   "   �� 5g   &   -   5        
 � Tag$ ID��5g   ;   .��6`   ID6d   5i      �� 5j      $	 200��6-    69   �� *Name+��5k   "   �� 5l   &   -   5        $
 Name��5l   ;   .��6b   Name6h   5n      �� 5o      $	 100��6-    69   �� *
Priorität+��5p   "   �� 5q   &   -   5        $ 
Priorität��5q   ;   .��6g   
Priorität6r   5s      �� 5t      $	 100��6-    69   �� *Adresse.Ort+��5u   "   �� 5v   &   -   5        $	 Ort��5v   ;   .��6a   Ort6f   5x      5z      ��5|      �� 5}       �� 
TargetType" ��6    .�5~      ��  �� Property � �� �� Value �	#FF2525256;   �� 5       � ��
 �����6;   �� 5�       � �� 5�   "   �� 5�   &    �� 
TargetType" ��66   .R�5�   *   �� 
������ $��0q�5�      #��z��5�      ._�5�   .   �� $��Stretch=�6C   #��z��6a   $
��Auto��6�   $��0q�6�    �� Content# z��6�   5�   +   5�   '   5�      -   5         �� Triggers 5�      P� 5�   "    P� Property �@J�IsMouseOver P� Value . . �5�   &   ��  � �� �	#FF3030306C   5�   #   P� 5�   "    �@��
IsSelected . . �5�   &   ��  � �� �	#FF1B1B1B6C   5�   #   5�      5�      5�      5�      5�      5�      