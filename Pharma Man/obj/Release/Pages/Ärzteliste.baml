   M S B A M L   `   `   `  ���� d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  E APharma Man, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Pharma_Man.UserControlsPharma_Man.UserControls 3clr-namespace:Pharma_Man.PagesPharma_Man.Pages "   Pharma_Man.Pages.Ärzteliste   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     cbingRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     >UserControls%clr-namespace:Pharma_Man.UserControls     0localclr-namespace:Pharma_Man.Pages       N� Title$  	Tagesplan�� � 	Resources  �� Source�� $ ../Resources.xaml?�%    .��� -    �Name$ 	Grid_Page��.+�� -   $ 
Grid_Modal�� K� ZIndex$ 2��$y�Hidden=�.+�� $ 3�� � RowDefinitions �� $��* ��� $
��auto ��� $��* � � ColumnDefinitions �� $��* ��� $��2* ��� $��* �.+��� -   $��3��$��3��$ 3������ .R��� 
���   �$��0�� J� Opacity$
 0.85Q���Z� � $��1��$��1��$ 3��.+��� $��5q�$��1q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   ShadowDepth$	 1Q�
   Opacity$
 1Q�   
BlurRadius$ 50Q�   	Direction$ 0Q�   Color$ White��� -   $ Grid_ModalContent�����	#F2191919$��6q� �� $
��auto ��� $��* �$	��100���� $��50 �.+�� $��0��.+��� $��Priorisieren��
�������$��18(��� -    ��Name$ lbl_Arzt_Name��$��DR. TEST�����	#FF5D5D5D$��14(�$��99,2,0,0q�$��Center=�$
��Left=��� -   $��Right=�$��50��   
FlatButton#��[  .��X� $��1��.+��� ���	#FF232323$	��0,1q���  �� Orientation$ Vertical=�$��20q�$��Center=�.+��� -   $ label��$��
Priorität��$
��Left=�$	��Top=�
�������$��0q��� -    ��Name$ tb_Prio��$
��Left=�$��23�� �� TextWrapping$
 Wrap=�$��TextBox��$	��Top=�$	��120��$��4,0,0,0q��� -	   $��2��$��
Auswählen��#��[  $��Right=�$	��100��$��
0,10,40,10q�� -
   $ Grid_Content��$��Stretch=�$��Stretch=�$��
10,0,10,10q� �� $	��100 ��� $��* �.+��� -   $ lbl_pageCaption��$��0��$��Ärzteliste��$��10,0,0,0q�  Label_PageCaption#��[ � $��1��.+��� -   $��ärzteListe��$��1��$ 4�����	#FF252525$��0q�
������� �� View ��  �� AllowsColumnReorder .  .P���  �� Width$	 120�� �� DisplayMemberBinding �� *ID+���� -   
 � Tag$ ID��.��ID�� $	 200�� �� *Name+���� -   $
 Name��.��Name�� $	 100�� �� *
Priorität+���� -   $ 
Priorität��.��
Priorität�� $	 100�� �� *Adresse.Ort+���� -   $	 Ort��.��Ort����  �� 
TargetType" ��.���  �� Property � �� �� Value �	#FF252525��  � ��
 �������  � �� ��  �� 
TargetType" ��.R��� 
������ $��0q�#��z��._��� $��Stretch=�#��z��$
��Auto��$��0q� �� Content# z��-    �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � �� �	#FF303030P�  �@��
IsSelected . . ���  � �� �	#FF1B1B1B