   M S B A M L   `   `   `  ���� d  `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF  E APharma Man, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Pharma_Man.UserControlsPharma_Man.UserControls 3clr-namespace:Pharma_Man.PagesPharma_Man.Pages #   Pharma_Man.Pages.Tagesplanung   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     cbingRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     >UserControls%clr-namespace:Pharma_Man.UserControls     0localclr-namespace:Pharma_Man.Pages       N� Title$  	Tagesplan�� � 	Resources  �� Source�� $ ../Resources.xaml?�%    .��� -    �Name$ 	Grid_Page��.+�� -   $ 
Grid_Modal�� K� ZIndex$ 2��$y�Hidden=�.+�� $ 3�� � RowDefinitions �� $��* ��� $��4* ��� $��* � � ColumnDefinitions �� $��* ��� $��8* ��� $��* �.+��� -   $��3��$��3��$ 3������ .R��� 
���   �$��0�� J� Opacity$
 0.85Q���Z� � $��1��$��1��$ 3��.+��� $��5q�$��1q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   ShadowDepth$	 1Q�
   Opacity$
 1Q�   
BlurRadius$ 50Q�   	Direction$ 0Q�   Color$ White��� -   $ Grid_ModalContent�����	#F2191919$��6q� �� $
��auto ��� $��* ��� $��50 �.+�� $��0��.+��� $��Ärzteauswahl��
�������$��18(��� $
��Left=�$��Center=�$	��200��$��	150,0,0,0q�.��Suchen�� -   $��Right=�$��50��   
FlatButton#��[  .��X�� -   $��ärzteListe��$��1��$ 4�����	#FF252525$��0q�
������� �� View ��  �� AllowsColumnReorder .  .P���  �� Width$	 120�� �� DisplayMemberBinding �� *ID+���� -   
 � Tag$ ID��.��ID�� $	 200�� �� *Name+���� -   $
 Name��.��Name�� $	 100�� �� *
Priorität+���� -	   $ 
Priorität��.��
Priorität����  �� 
TargetType" ��.���  �� Property � �� �� Value �	#FF252525��  � ��
 �������  � �� ��  �� 
TargetType" ��.R��� 
������ $��0q�#��z��._��� $��Stretch=�#��z��$
��Auto��$��0q� �� Content# z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � �� �	#FF303030P�  �@��
IsSelected . . ���  � �� �	#FF1B1B1B�� -
   $��2��$��
Auswählen��#��[  $��Right=�$	��100��$��
0,10,40,10q�� -   $ Grid_Time_Modal��$ 2��$y�Hidden=�.+�� $ 3�� �� $��* ��� $
��auto ��� $��* � �� $��* ��� $��2* ��� $��* �.+��� -   $��3��$��3��$ 3������ .R��� 
���   �$��0��$
 0.85Q���Z� � $��1��$��1��$ 3��.+��� $��5q�$��1q�
�������   $	 1Q�$
 1Q�$ 50Q�$ 0Q�$ White��� -   $ Grid_Time_ModalContent�����	#F91D1D1D$��6q� �� $
��auto ��� $��* ��� $
��auto �.+�� $��0��.+��� $!��Geschätzte Dauer eintragen��
�������$��18(�$��10,0,0,0q��� -   $��Right=�$��50��#��[  .��X� $��1��.+��� ���	#FF232323$	��0,1q��� $��Minuten.�����	#FF939393$��	118,6,0,6q�$
��Left=�$��Center=��� -    ��Name$ tb_Time��$
��Left=�$
��auto��$��
40,20,0,20q� �� TextWrapping$
 Wrap=�$��TextBox��$��Center=����	#FFCFCFCF���	#FF383838$��80��� $��2��.+��� -   $��Bestätigen��$��Right=�$	��100��$��30��#��[  $��0,5,30,5q�� -   $ Grid_Content��$��Stretch=�$��Stretch=�$��
10,0,10,10q� �� $	��100 ��� $��* �.+��� -    ��Name$ lbl_pageCaption��$��0��$��	Tagesplan��$��10,0,0,0q�  Label_PageCaption#��[ �� -   $ lbl_date��$��0��$��DATUM��$��
230,20,0,0q�$��24(����	#FF979797$
��Left=�$��Center=�� $��1�� �� $��10 ��� $	��400 ��� $��20 ��� $	��400��$��3* �.+�� $��1�� �� $
��auto ��� $��* ��� $��50 ��� $��50 �.+�� $��0��.+��� -   $ docList_caption��$��Geplante Besuche��$��Stretch=�$	��Top=�$��32��$��18(�
�������$��-4,0,0,0q�� $��1��.+��� ���	#FF212121�� -    ��Name$ docList��  ListBoxBesuche#��[ 
���������.  $	��0,2q����� .R�� ���	#FF2E2E2E$��40�� �� $��35 ��� $��* ��� $��40 ��� $��40 ��� $��40 �.+��� $��0��$��20(�$��Center=�$��Center=����� *ID+�� $��1��$��20(�$��Center=�$��5,0,0,0q����� *	Arzt.Name+)  #Pharma_Man.UserControls.ImageButton  )  #Pharma_Man.UserControls.ImageButton   PreviewMouseLeftButtonUp OpenTimeModal$��2��    Image$  ../Ressources/Clock.png��   LockListItem$��3��$  ../Ressources/Lock.png��   RemoveBesuch$��4��$  ../Ressources/remove.png��� -   $ Grid_ListControls��$��2�����	#FF212121 �� $��1* ��� $��1* ��� $��1* �.+�  $ 
MoveItemUp��$��0��$  ../Ressources/arrowUp.png��$��1,0,1,1q�  $ MoveItemDown��$��2��$!  ../Ressources/arrowDown.png��$��0,0,1,1q�  $ 	ShowModal��$��1��$  ../Ressources/add.png��$��0,0,1,1q�� -   $ Grid_Buttons��$��3�����	#FF212121 �� $��1* ��� $��1* ��� $��1* �.+��� -   $��0��$��	Speichern��#��[  $��1,0,1,1q��� -   $��1��$��Route optimieren��#��[  $��0,0,1,1q��� -   $��2��$��Abschließen��#��[  $��0,0,1,1q�� -   $��map_container��$��3�� �� $
��auto ��� $��* ��� $
��auto �.+��� -   $ map_caption��$��0��$��Karte��$��Stretch=�$	��Top=�$��32��$��18(�
�������$��-4,0,0,0q�� $��1��.+�'   !Microsoft.Maps.MapControl.WPF.Map  -   $��BingMap��+   %Microsoft.Maps.MapControl.WPF.MapCore!   Center<   6Microsoft.Maps.MapControl.WPF.Design.LocationConverter$! 49.145467, 9.219008 "   CredentialsProviderT   NMicrosoft.Maps.MapControl.WPF.Design.ApplicationIdCredentialsProviderConverter$F" @AggNeGV4ySngk7sHLrggyU7kbsyPehieMJrGhZtaJ7Eubm5qe5G8YkRH2bssu6PJ #   Mode;   5Microsoft.Maps.MapControl.WPF.Design.MapModeConverter$
# Road $   	ZoomLevel$$ 13Q�� $��2��.+��� -   $ lbl_map_distance��$��Fahrtdistanz: 30km��$
��Left=�$��Bottom=�
�������$��0,0,0,24q��� -   $ lbl_geschätzteDauer��$%��Geschätzte Dauer: 1Std. 43Min.��$
��Left=�$��Bottom=�
�������$��0,0,0,7q�