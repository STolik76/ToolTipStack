   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Assistant	Assistant      Assistant.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Assistant     -     � Title$)  #Помошник технолога��$	��450��$	��800��.��� .+��� $��0��.@��� $��
Файлы��.���  � RowDefinitions �� $��* ��� $
��auto �.+��� -    ��Name$ icFiles��$��0������ .R��  � Orientation$ Vertical=����� .R��� $��1q�
���   �$��1q� �� Padding$ 0q�._��� .+��� $
��Bold"�
�������
���   �$��2,0,2,0q����� *	DirName+�� $��0q�$��0,0,2,0q�$��0q����� *Files+���� .���  �� Property � |� �� Value .  ���� .R��� $��1q�����  �� Triggers ��  �� Binding �� *Status+	 �� Value	 None.a���  � �� �	#FF616161��  �� *Status+		 Exist.a���  � �� �	#FF165524��  �� *Status+
	 Update.a���  � �� �	#FF162455��  �� *Status+	 Expired.a���  � �� �	#FF551616.+��� $
��Left=�$��0q�$��2,0,2,0q�
����������� *
FileName+�� $��Right=�$��0q�$��2,0,2,0q�$��8(�
����������� *
DateUpdate+�� $��1��
 �� Orientation$
 
Horizontal=��� $��Регламент���� $��Тест��.���� 