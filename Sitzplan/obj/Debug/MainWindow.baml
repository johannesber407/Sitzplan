   M S B A M L   `   `   `  ����C  ?Sitzplan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SitzplanSitzplan  5   	       Sitzplan.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Sitzplan     5   	     � Title$  
MainWindow��5   	    � SizeToContent$ WidthAndHeight=�6   .��5
      � .+�5   
   o� .@�5      m� $��Datei��6   .@�5      m� -   5        $��	Speichern��5      $��Speichern Unter��6-   6F   m� 5      -   5        $��Oeffnen��5      $��Datei Öffnen��6*   6@   5      5      �� 5   
    �� Orientation$ Vertical=�6   $��0,10,0,0q�6,   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Schüler��6   $��10q�6,   .��5      �� $ Vertical=�6!   $��10q�68   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��10q�6$   .��60   Schüler eintragen:6D   �� 5      -   5        $��SchuelerEingeben��5   &   $	��180��6>   $��10q�6J   6W   �� 5      -   5        $��NeuerSchueler��5   %   $��Eingeben��6:   $��10q�6M   6Y   5      �� 5      $ 
Horizontal=�6%   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridKlassenliste��5   '      AutoGenerateColumns .     
IsReadOnly .    Columns 5   "   0  *System.Windows.Controls.DataGridTextColumn  5   &   ,  &System.Windows.Controls.DataGridColumn   Header$ Nr��69   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *Nr+  5   &   $
 Name��69    6H   �� *Name+  5   &   $ Wunsch 1��69    6L   �� *	Wunsch1+  5    &   $ Wunsch 2��69    6L   �� *	Wunsch2+  5!   &   $ Wunsch 3��69    6L   �� *	Wunsch3+  5"   &   $ Wunsch 4��69    6L   �� *	Wunsch4+  5#   &   $ Wunsch 5��69    6L   �� *	Wunsch5+5'      5)      5+      5,      �� 5-      $ 
Horizontal=�6   .+�5.      �� $ Vertical=�6!   .+�5/      �� $ 
Horizontal=�6%   .+�50      �� $ Vertical=�6)   .+�51      �� $ 
Horizontal=�6%   .+�52      �� $��Wünsche��6'   $��10q�68   .��53   "   �� $ 
Horizontal=�6-   $��10q�6F   .+�54   &   �� $ Vertical=�61   .+�55   *   �� $ 
Horizontal=�65   .+�56   .   �� $��10q�64   .��6@   	Schüler:6J   �� 57   .   -   5        $��ComboboxSchueler��57   7   $��10q�6O   $	��180��6[   $��25��6g   $	��Top=�6s   ��6�   �� *
Schueler+59   /   5:   +   �� 5;   *   $ Vertical=�65   .+�5=   .   �� -   5        $��WuenscheNaechsterSchueler��5=   Q   $��Bottom=�66   $	��100��6r   $��Nächster Schüler��6~   {�.  6�   �� 5?   .   -   5        $��SchuelerLoeschen��5?   M   $��10q�65   $	��100��6A   {�.  .��6�   Schüler löschen6�   5@   +   5B   '   �� 5C   &   $ Vertical=�61   .+�5D   *   �� $ 
Horizontal=�65   .+�5E   .   �� $��10q�64   .��6@   	Wunsch 1:6K   �� 5F   .   -	   5        $��ComboboxWunsch1��5F   7   $��10q�6N   $	��180��6Z   {�.  ��6x   �� *
Schueler+6�   5G   +   �� 5H   *   $ 
Horizontal=�65   .+�5I   .   �� $��10q�64   .��6@   	Wunsch 2:6K   �� 5J   .   -
   5        $��ComboboxWunsch2��5J   7   $��10q�6N   $	��180��6Z   {�.  ��6x   �� *
Schueler+6�   5K   +   �� 5L   *   $ 
Horizontal=�65   .+�5M   .   �� $��10q�64   .��6@   	Wunsch 3:6K   �� 5N   .   -   5        $��ComboboxWunsch3��5N   7   $��10q�6N   $	��180��6Z   {�.  ��6x   �� *
Schueler+6�   5O   +   �� 5P   *   $ 
Horizontal=�65   .+�5Q   .   �� $��10q�64   .��6@   	Wunsch 4:6K   �� 5R   .   -   5        $��ComboboxWunsch4��5R   7   $��10q�6N   $	��180��6Z   {�.  ��6x   �� *
Schueler+6�   5S   +   �� 5T   *   $ 
Horizontal=�65   .+�5U   .   �� $��10q�64   .��6@   	Wunsch 5:6K   �� 5V   .   -   5        $��ComboboxWunsch5��5V   7   $��10q�6N   $	��180��6Z   {�.  ��6x   �� *
Schueler+6�   5W   +   5X   '   5Y   #   5Z      5^      �� 5_      $ 
Horizontal=�6%   .+�5`      �� $ Vertical=�6)   .+�5a   "   �� $��Sitzplan��6+   $��10q�6=   .��5b   &     -   5        $ DataGridErgebnis��5b   /    .  .  5c   *   5h   '   5i   #   5j      �� 5k      $ Vertical=�6)   .+�5l   "   �� $��Art der Sitzordnung��6+   $��10q�6H   .��5m   &   �� $ Vertical=�61   .+�5n   *   	� -   5        $��Paare��5n   h   	 	� 	GroupName$	 ArtDerSitzOrdnung��66   $��	2er Paare��6T   
 d� 	IsChecked$

 TrueY�6u   	� 5o   *   -   5        $��ZweiDreiZwei��5o   d   $	 ArtDerSitzOrdnung��66   $��2 3 2��6T   6d   	� 5p   *   -   5        $��ZweiVierZwei��5p   d   $	 ArtDerSitzOrdnung��66   $��2 4 2��6T   6d   	� 5q   *   -   5        $��VierVier��5q   b   $	 ArtDerSitzOrdnung��66   $	��4 4��6T   6b   	� 5r   *   -   5        $��
FuenfFunef��5r   b   $	 ArtDerSitzOrdnung��66   $	��5 5��6T   6b   5s   '   5t   #   5u      5v   #   5w      �� 5x      $ Vertical=�6)   .+�5y   "   �� $ 
Horizontal=�6-   .+�5z   &   �� $��
Blockieren��6/   $��10q�6C   .��5{   *   �� $ Vertical=�65   .+�5|   .   �� $ 
Horizontal=�69   .+�5}   2   �� .��68   Blockierung beachten6N   �� 5~   2   -   5        $��BlockierungBeachten��5~   ;   $

 TrueY�6V   $��7q�6g   6�   5�   /   �� 5�   .   $��Center=�64   .��6Q   )'Diese Kombination soll verboten werden:6z   �� 5�   .   -   5        $��Blockieren1��5�   7   $	��180��6J   ��6V   �� *
Schueler+6�   �� 5�   .   $��Center=�64   .��6Q   mit6V   �� 5�   .   -   5        $��Blockieren2��5�   8   $	��180��6K   {�.  ��6W   �� *
Schueler+6�   �� 5�   .   -   5        $��BlockierenEingeben��5�   5   $��Eingeben��6O   $	��100��6b   $��10q�6�   {�.  6�   �� 5�   .   -   5        $��AlleBlockierungenLoeschen��5�   5   $!��Alle Blockierungen löschen��6V   $��10q�6�   $	��160��6�     5�   .   -   5        $ DataGridBlockiert��5�   7    .  .  5�   2     5�   6   5�   /   5�   +   5�   '   5�   #   �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   �� $��Sitznachbarn erzwingen��6/   $��10q�6O   .��5�   *   �� $ Vertical=�65   .+�5�   .   �� $ 
Horizontal=�69   .+�5�   2   �� .��68   Sitznachbarn beachten6O   �� 5�   2   -   5        $��SitznachbarnBeachten��5�   ;   $
 FalseY�6W   $��7q�6i   6�   5�   /   �� 5�   .   $��Center=�64   .��6Q   -+Diese Schüler sollen nebeneinander sitzen:6}   �� 5�   .   -   5        $��Sitznachbar1��5�   7   $	��180��6K   {�.  ��6W   �� *
Schueler+6�   �� 5�   .   $��Center=�64   .��6Q   mit6V   �� 5�   .   -   5        $��Sitznachbar2��5�   8   $	��180��6L   {�.  ��6X   �� *
Schueler+6�   �� 5�   .   -   5        $��SitznachbarnEingeben��5�   5   $��Eingeben��6Q   $	��100��6d   $��10q�6�   {�.  6�   �� 5�   .   -   5        $��AlleSitznachbarnLoeschen��5�   5   $ ��Alle Sitznachbarn löschen��6U   {�.  $��10q�6�   $	��160��6�     5�   .   -   5        $ DataGridSitznachbarn��5�   7    .  . {�.   5�   2     5�   6   5�   /   �� 5�   .   $ 
Horizontal=�69   $��0, 10, 0, 0q�6R   .+�5�   2   �� .��68   Gewichtung:6E   �� 5�   2   -    5        $��ComboBoxGewichtung��5�   ;    �� SelectedIndex$ 3��6U   {�.  6{   5�   /   5�   +   5�   '   5�   #   5�      5�      �� 5�      $ 
Horizontal=�6%   $��Center=�6>   .+�5�      �� -!   5        $��ButtonBerechnen��5�   %   $��
_Berechnen��6<   $��10q�6o   �� 5�      -"   5        $��ButtonBeenden��5�   %   $��B_eenden��6:   $��10q�6i   �� 5�      $��Center=�6$   .��6?   Iterationen:6M   �� 5�      -#   5        $��Iterationen��5�   '   $	��180��6;   $��10q�6G   $ 4��6m   ��6S   �� *I+6m   �� 5�      $ Vertical=�6)   .+�5�   "   	� -$   5        $��
trueRandom��5�   K   $	 IterationenRadio��6.   $��Zufällige Kombination��6]   6}   	� 5�   "   -%   5        $��Normal��5�   K   $	 IterationenRadio��6.   $��Bestmögliche Kombination��6Y   $

 TrueY�6|   6�   5�      5�      5�      5�      5�      �� 5�      $��Right=�6   .��60   Johannes Bertsch6B   5�      5�      5�      