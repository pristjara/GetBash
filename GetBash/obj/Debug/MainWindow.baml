   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   B >GetBash, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GetBashGetBash 5   	      GetBash.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   J FWpfAnimatedGif, Version=1.4.15.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	   !localclr-namespace:GetBash  5   	      AccentColorBrush(   "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  �   5
   	    � SizeToContent$ Height=�5       � Title$ Bash: /dev/random ��5   	   $	��250��6$   $	��590��61   $	��800��6=    � 
ResizeMode$ CanMinimize=�5       � Icon$ favicon.ico��60   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��75 �6#   �� 5      $��75 �6#   �� 5      $	��120 �6#   �� 5      $��60 �6#   �� 5      $��80 �6#   �� 5      $��80 �6#   �� 5      $��80 �6#   .+�5!      �� -   5        $��
lblCounter��5!      $��0��6&   $��5q�66   .��6A   
lblCounter6M   �� 5"      -   5        $��	lblRating��5"      $��1��6%   $��5q�65   $
��Bold"�6@   .��6R   	lblRating6]   �� 5#      -   5        $��lblData��5#      $��2��6#   $��5q�63   .��6>   	lblData6G   �� 5$      -   5        $��
tb_content��5$   3   $��3��6   $��5q�6(   �� 5%      -   5         �Name$ hl_link��5%      .�6U   .�5&      �� -   5        $��lblLink��5&   .   $��lblLink��6   6.   5'      5(      �� 5*      -   5        $��btnPrev��5*      $��4��6$   $��0��64   $��5q�6A   $��Previous��6L   6_   �� 5+      -   5        $��btnShare��5+      $��5��6%   $��0��65   $��5q�6B   .��6d   Share6k   �� 5,      -	   5        $��btnNext��5,      $��6��6$   $��0��64   $��5q�6A   $
��Next��6b   5-      �� 50   
   $��1��6   $	��150��6    .��51      �� -
   5        $��txtQuote��52       �� 
IsReadOnly . 	 �� AcceptsReturn	 . 
�������
 �� TextWrapping$

 Wrap=�56      " �� VerticalScrollBarVisibility$
 Auto=�57      $��16(�58      .��6     � ContextMenu 5:      �� 5;      -   5        $��txtQueryContextMenu��5<      .@�5>      m� -   5        $��cxtCopy��5@      $
��Copy��5?      5A      �� 5C      m� 5D      -   5        $��	cxtGoogle��5F      $��	Google it��5E      5G      m� 5I      -   5        $��cxtSave��5K      $��Save it��5J      5L      �� 5N      m� 5O      -   5        $��cxtOpen��5Q      $��	Open File��5P      5R      5T      .��5U   '   5V      5W      �� 5Y   
   -   5        $��loading_panel��5Y   B   $��0��6   $��0��6!   $��2��61    �� TextAlignment$ Center=�6W   $��34(�6n   $y�Hidden=�6|    �� Padding$ 0 120q�6�   ��5Z      �� 5[       �� Color$ White��6"    �� Opacity$
 0.85Q�60   )   #MahApps.Metro.Controls.ProgressRing  5]      -   5        $��progressRing��5]   $      IsActive .  68   5^      5_      5`      