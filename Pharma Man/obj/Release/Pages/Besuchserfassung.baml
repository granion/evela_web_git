   M S B A M L   `   `   `  ���� E  APharma Man, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Pharma_Man.PagesPharma_Man.Pages  A%clr-namespace:Pharma_Man.UserControlsPharma_Man.UserControls  *    $Pharma_Man.Pages.BesuchsplanErfassen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Pharma_Man.Pages     >UserControls%clr-namespace:Pharma_Man.UserControls       N� Title$  BesuchsplanErfassen�� � 	Resources  �� Source�� $ ../Resources.xaml?�%    .��� -    �Name$ 	Grid_Page��.+�� -   $ 
Grid_Modal�� K� ZIndex$ 2��$y�Hidden=�.+�� $ 3�� � RowDefinitions �� $��* ��� $
��auto �$	��600���� $��* � � ColumnDefinitions �� $��* ��� $	��450 ��� $��* �.+��� -   $��3��$��3��$ 3������ .R��� 
���   �$��0�� J� Opacity$
 0.85Q���Z� � $��1��$��1��$ 3��.+��� $��5q�$��1q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   ShadowDepth$	 0Q�
   Opacity$
 1Q�   
BlurRadius$ 30Q�   	Direction$ 0Q�   Color$ White��   RenderingBias$ Quality=�� -   $ Grid_ModalContent�����	#F22E2E2E$��6q� �� $
��auto ��� $��* ��� $��40 �.+�� -   $ Grid_Modal_TopRow��$��0�� �� $
��auto ��� $��* ��� $��50 �.+��� $��0��$��	Tagesplan��
�������$��20(�$��0,0,-2,0q��� -    ��Name$  Grid_Modal_TopRow_lbl_Date��$��1��$��für den 22.22.2222�����	#FF8F8F8F$��14(�$��-2,7,0,0q�� $��1��.+��� -    ��Name$ 
besuchList��   ListBoxBesuche#��[  
�������$
��Auto=���.  ���� .R�� ���	#FF323232$��40�� �� $��* ��� $��40 ��� $��40 �.+��� $��0��$��20(�$��Center=�$��5,0,0,0q����� *	Arzt.Name+����  �� 
TargetType" �� �� BasedOn# [B��.�-   � $��2��.+�)   #Pharma_Man.UserControls.ImageButton  )   #Pharma_Man.UserControls.ImageButton   PreviewMouseLeftButtonUp$ NeuerBesuch��$��2��   Image$ ../Ressources/add.png��� -	   $ Page_Content��$��Stretch=�$��Stretch=�$��10q� �� $	��100 ��� $��* �.+��� -
   $ page_caption��$��0��$��Besuchserfassung��$��10,0,0,0q�  Label_PageCaption#��[ (� $��1��$��Stretch=�$��Stretch=�.��&   System.Windows.Controls.Calendar  -     Name$ calendar�����	#FF75C335
���������Z� 