using System;
using System.Text.RegularExpressions;

namespace bankamatik_kurs_proje
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();
            atm.Start();
        }
    }

    class ATM
    {
        private double balance = 2500;
        private string pin = "ab18";
        private int attempts = 0;
        private bool isLoggedIn = false;

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\nBankamatik Menü:");
                Console.WriteLine("Kartlı işlem 1");
                Console.WriteLine("Kartsız işlem 2");
                Console.Write("Seçiminizi yapın: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    CardTransaction();
                }
                else if (choice == "2")
                {
                    CardlessTransaction();
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim!");
                }
            }
        }

        private void CardTransaction()
        {
            Console.WriteLine("\nKartlı İşlem");
            Console.Write("Şifre: ");
            string enteredPin = Console.ReadLine();

            if (enteredPin == pin)
            {
                isLoggedIn = true;
                MainMenu();
            }
            else
            {
                attempts++;
                if (attempts >= 3)
                {
                    Console.WriteLine("Şifreyi 3 kere yanlış girdiniz. Sistemden atılıyorsunuz.");
                    return;
                }
                Console.WriteLine("Yanlış şifre. Tekrar deneyin.");
                CardTransaction();
            }
        }

        private void MainMenu()
        {
            while (isLoggedIn)
            {
                Console.WriteLine("\nAna Menü:");
                Console.WriteLine("Para Çekmek için 1");
                Console.WriteLine("Para Yatırmak için 2");
                Console.WriteLine("Para Transferleri 3");
                Console.WriteLine("Eğitim Ödemeleri 4");
                Console.WriteLine("Ödemeler 5");
                Console.WriteLine("Bilgi Güncelleme 6");
                Console.Write("Seçiminizi yapın: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Withdraw();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Transfer();
                        break;
                    case "4":
                        Console.WriteLine("Eğitim Ödemeleri sayfası arızalı");
                        break;
                    case "5":
                        PayBills();
                        break;
                    case "6":
                        ChangePin();
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }

        private void Withdraw()
        {
            Console.Write("Çekmek istediğiniz tutarı girin: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (amount > balance)
                {
                    Console.WriteLine("Yetersiz bakiye!");
                }
                else
                {
                    balance -= amount;
                    Console.WriteLine($"Yeni bakiye: {balance} TL");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz tutar!");
            }
            PostTransaction();
        }

        private void Deposit()
        {
            Console.WriteLine("Para yatırmak için:");
            Console.WriteLine("Kredi Kartına 1");
            Console.WriteLine("Kendi Hesabınıza 2");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Kredi kartı numarasını girin (12 haneli): ");
                string cardNumber = Console.ReadLine();
                if (cardNumber.Length == 12 && long.TryParse(cardNumber, out _))
                {
                    Console.Write("Yatırmak istediğiniz tutarı girin: ");
                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        balance += amount;
                        Console.WriteLine($"Yeni bakiye: {balance} TL");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tutar!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz kredi kartı numarası!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Yatırmak istediğiniz tutarı girin: ");
                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    balance += amount;
                    Console.WriteLine($"Yeni bakiye: {balance} TL");
                }
                else
                {
                    Console.WriteLine("Geçersiz tutar!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }

        private void Transfer()
        {
            Console.WriteLine("Para Transferleri:");
            Console.WriteLine("Başka Hesaba EFT 1");
            Console.WriteLine("Başka Hesaba Havale 2");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("EFT numarasını girin (tr ve 12 haneli sayı): ");
                string eftNumber = Console.ReadLine();
                if (Regex.IsMatch(eftNumber, @"^tr\d{12}$"))
                {
                    Console.Write("Yatırmak istediğiniz tutarı girin: ");
                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        balance -= amount;
                        Console.WriteLine($"Yeni bakiye: {balance} TL");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tutar!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz EFT numarası!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Hesap numarasını girin (11 haneli): ");
                string accountNumber = Console.ReadLine();
                if (accountNumber.Length == 11 && long.TryParse(accountNumber, out _))
                {
                    Console.Write("Göndermek istediğiniz tutarı girin: ");
                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        balance -= amount;
                        Console.WriteLine($"Yeni bakiye: {balance} TL");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tutar!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz hesap numarası!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }

        private void PayBills()
        {
            Console.WriteLine("Ödemeler:");
            Console.WriteLine("Elektrik Faturası 1");
            Console.WriteLine("Telefon Faturası 2");
            Console.WriteLine("İnternet Faturası 3");
            Console.WriteLine("Su Faturası 4");
            Console.WriteLine("OGS Ödemeleri 5");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
            {
                Console.Write("Fatura tutarını girin: ");
                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    balance -= amount;
                    Console.WriteLine($"Yeni bakiye: {balance} TL");
                }
                else
                {
                    Console.WriteLine("Geçersiz tutar!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }

        private void ChangePin()
        {
            Console.Write("Yeni şifreyi girin: ");
            string newPin = Console.ReadLine();
            if (newPin.Length < 4)
            {
                Console.WriteLine("Şifre en az 4 haneli olmalıdır!");
            }
            else
            {
                pin = newPin;
                Console.WriteLine("Şifre başarıyla değiştirildi!");
            }
            PostTransaction();
        }

        private void PostTransaction()
        {
            Console.WriteLine("Ana Menüye dönmek için 9");
            Console.WriteLine("Çıkmak için 0");
            string choice = Console.ReadLine();

            if (choice == "9")
            {
                MainMenu();
            }
            else if (choice == "0")
            {
                isLoggedIn = false;
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
        }

        private void CardlessTransaction()
        {
            Console.WriteLine("\nKartsız İşlem");
            Console.WriteLine("CepBank Para Çekmek 1");
            Console.WriteLine("Para Yatırmak için 2");
            Console.WriteLine("Kredi Kartı Ödeme 3");
            Console.WriteLine("Eğitim Ödemeleri 4");
            Console.WriteLine("Ödemeler 5");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CardlessWithdraw();
                    break;
                case "2":
                    CardlessDeposit();
                    break;
                case "3":
                    CardlessPayment();
                    break;
                case "4":
                    Console.WriteLine("Eğitim Ödemeleri sayfası arızalı");
                    break;
                case "5":
                    CardlessPayBills();
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }

        private void CardlessWithdraw()
        {
            Console.Write("TC Kimlik Numaranızı girin: ");
            string tcNumber = Console.ReadLine();
            Console.Write("Cep Telefonu Numaranızı girin: ");
            string phoneNumber = Console.ReadLine();
            if (tcNumber.Length == 11 && phoneNumber.All(char.IsDigit))
            {
                Console.WriteLine("1000 TL ödeme yapılacaktır.");
            }
            else
            {
                Console.WriteLine("Geçersiz TC Kimlik Numarası veya Telefon Numarası!");
            }
            PostTransaction();
        }

        private void CardlessDeposit()
        {
            Console.WriteLine("Para yatırmak için:");
            Console.WriteLine("Nakit ödeme 1");
            Console.WriteLine("Hesaptan ödeme 2");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Kredi kartı numarasını girin (12 haneli): ");
                string cardNumber = Console.ReadLine();
                if (cardNumber.Length == 12 && long.TryParse(cardNumber, out _))
                {
                    Console.Write("TC Kimlik Numaranızı girin (11 haneli): ");
                    string tcNumber = Console.ReadLine();
                    if (tcNumber.Length == 11 && long.TryParse(tcNumber, out _))
                    {
                        Console.WriteLine("Nakit olarak ödeme gerçekleştirildi.");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz TC Kimlik Numarası!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz kredi kartı numarası!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Kredi kartı numarasını girin (12 haneli): ");
                string cardNumber = Console.ReadLine();
                Console.Write("Hesap numarasını girin: ");
                string accountNumber = Console.ReadLine();
                if (cardNumber.Length == 12 && long.TryParse(cardNumber, out _) && long.TryParse(accountNumber, out _))
                {
                    Console.WriteLine("Hesaptan ödeme gerçekleştirildi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz kart veya hesap numarası!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }

        private void CardlessPayment()
        {
            Console.WriteLine("Para Transferleri:");
            Console.WriteLine("Başka Hesaba EFT 1");
            Console.WriteLine("Başka Hesaba Havale 2");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("EFT numarasını girin (tr ve 12 haneli sayı): ");
                string eftNumber = Console.ReadLine();
                if (Regex.IsMatch(eftNumber, @"^tr\d{12}$"))
                {
                    Console.Write("Yatırmak istediğiniz tutarı girin: ");
                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        Console.WriteLine("EFT işlemi gerçekleştirildi.");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tutar!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz EFT numarası!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Hesap numarasını girin (11 haneli): ");
                string accountNumber = Console.ReadLine();
                if (accountNumber.Length == 11 && long.TryParse(accountNumber, out _))
                {
                    Console.Write("Göndermek istediğiniz tutarı girin: ");
                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        Console.WriteLine("Havale işlemi gerçekleştirildi.");
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tutar!");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz hesap numarası!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }

        private void CardlessPayBills()
        {
            Console.WriteLine("Ödemeler:");
            Console.WriteLine("Elektrik Faturası 1");
            Console.WriteLine("Telefon Faturası 2");
            Console.WriteLine("İnternet Faturası 3");
            Console.WriteLine("Su Faturası 4");
            Console.WriteLine("OGS Ödemeleri 5");
            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
            {
                Console.Write("Fatura tutarını girin: ");
                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    Console.WriteLine("Fatura ödemesi gerçekleştirildi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz tutar!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            PostTransaction();
        }
    }
}

