   M S B A M L   `   `   `  ���� E  APharma Man, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pharma_Man.PagesPharma_Man.Pages  &     Pharma_Man.Pages.Besuch_erfassen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Pharma_Man.Pages       N� Title$  Besuch erfassen�� � 	Resources  �� Source�� $ ../Resources.xaml?�%    .��� .+�� -    �Name$ 
Grid_Modal�� K� ZIndex$ 2��$y�Hidden=�.+�� $ 3�� � RowDefinitions �� $��* ��� $��4* ��� $��* � � ColumnDefinitions �� $��* ��� $��8* ��� $��* �.+��� -   $��3��$��3��$ 3������ .R��� 
���   �$��0�� J� Opacity$
 0.85Q���Z� � $��1��$��1��$ 3��.+��� $��5q�$��1q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   ShadowDepth$	 1Q�
   Opacity$
 1Q�   
BlurRadius$ 50Q�   	Direction$ 0Q�   Color$ White��� -   $ Grid_ModalContent�����	#F2191919$��6q� �� $
��auto ��� $��* ��� $��50 �.+�� $��0��.+��� $��Ärzteauswahl��
�������$��18(��� $
��Left=�$��Center=�$	��200��$��	150,0,0,0q�.��Suchen�� -   $��Right=�$��50��   
FlatButton#��[  .��X�� -   $��ärzteListe��$��1��$ 4�����	#FF252525$��0q�
������� �� SelectionMode$ Single=� �� View ��  �� AllowsColumnReorder .  .P���  �� Width$	 120�� �� DisplayMemberBinding �� *ID+���� -   
 � Tag$ ID��.��ID�� $	 200�� �� *Name+���� -   $
 Name��.��Name�� $	 100�� �� *
Priorität+���� -   $ 
Priorität��.��
Priorität����  �� 
TargetType" ��.���  �� Property � �� �� Value �	#FF252525��  � ��
 �������  � �� ��  �� 
TargetType" ��.R��� 
������ $��0q�#��z��._��� $��Stretch=�#��z��$
��Auto��$��0q� �� Content# z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � �� �	#FF303030P�  �@��
IsSelected . . ���  � �� �	#FF1B1B1B�� -	   $��2��$��
Auswählen��#��[  $��Right=�$	��100��$��
0,10,40,10q�� -
   $ Grid_Content��$��Stretch=�$��Stretch=�$��10q� �� $	��100 ��� $��* �.+��� -    ��Name$ page_caption��$��0��$��Besuch erfassen��$��10,0,0,0q�  Label_PageCaption#��[ � $��1��$��20,0,0,0q�.+��  �� -    �Name$ 
Datum_Zeit��$��* ��� -   $ Thema��$��* ��� -   $ Ärztemuster_Broschüre��$��* ��� -   $ Notiz��$��* ��� -   $ BUTTONS��$
��auto �.+�� $��0��.+��� -    ��Name$ sp_Zeit�� �� Orientation$ Vertical=�$	��Top=�.+��� -   $ lbl_Zeit��$��Datum & Zeit��  
Label_Name#��[ �� $��Stretch=�$ 
Horizontal=�.+��� $��Am��$��10,0,0,0q����	#FF8B8B8B�� -     ��Name$  tb_Zeit��! �� TextWrapping$
! Wrap=�$
��auto��$��Center=�$��30���� $��, von��$��10,0,0,0q����	#FF8B8B8B�� -   $  tb_Von��$
! Wrap=�$
��auto��$��Center=�$��30���� $	��bis�����	#FF8B8B8B�� -   $  tb_Bis��$
! Wrap=�$
��auto��$��Center=�$��30���� $
��Uhr.�����	#FF8B8B8B�� $
��Arzt��#��[ � $��30�� �� $	��300 ��� $��* �.+��� -   $  tb_Arzt��$��0��$
! Wrap=�$	��300��$
��Left=��� -   $��1��$��Arzt auswählen��#��[  $	��100��$
��Left=�$��13,0,0,0q�$��1q����	#FF212121� $��1��.+��� -   $ sp_Thema��$ Vertical=�$	��Top=�.+��� -   $ 	lbl_Thema��$��Thema��#��[ �� -   $  tb_Thema��$
! Wrap=�$	��300��$
��Left=�$��30��� $��2��.+��� -   $ sp_Ärztemuster��$ Vertical=�$	��Top=�$��Stretch=�.+��� -   $ lbl_Ärztemuster��$ ��Ärztemuster & Broschüren��#��[ �� -   " ��Name$" lb_Ärztemuster��$��30��$
��Left=�$	��300��� $��3��.+��� -   $ 
sp_Notizen��$ Vertical=�$	��Top=�$��Stretch=�.+��� -   $ lbl_Notizen��$��Notiz��#��[ �� -    $  
tb_Notizen��$
��Left=�$	��300��$��30��� $��4��.+�� -!   $��Grid_Buttons��$
��0,20q�.+��� -"   # ��Name$# btn_Speichern��$��Besuch speichern��$
��Left=�$��Bottom=�$	��120��$��50��#��[  $��1q����	#FF212121�� -#   $# btn_BesuchAbschließen��$y�Visible=�$��Besuch abschließen��$
��Left=�$��Bottom=�$	��120��$��50��$��	150,0,0,0q�#��[  $��1q����	#FF212121