   M S B A M L   `   `   `  ����D  @IotDevice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:IotDevice	IotDevice  5   	       IotDevice.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:IotDevice     5   	     � Title$  
IOT DEVICE��5   	   $	��210��6   $	��350��6)   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      �� $��DeviceId��6   $��9(�6(   �� 5      -   5         ��Name$ tblockDeviceId��5      $��0 0 0 20q�60   �� 5      -   5         ��Name$ 
btnConnect��5      $�� ��6)   $��10 10 10 10q�64   ${�False��6c   5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0 20 0 0q�6;   .+�5      �� $��Status: ��6   �� 5      -   5        $ 
tblockInfo��5      $��10 0 0 0q�6,   5      5      5      