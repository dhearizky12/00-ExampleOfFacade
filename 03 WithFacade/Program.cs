namespace WithFacade
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product()
        {

        }

        public void GetProductDetails()
        {
            Console.WriteLine($"Product");
            Console.WriteLine($"- Name : {Name}");
            Console.WriteLine($"- Price : {Price:C}");
        }
    }

    public enum EPaymentMethod
    {
        Unknown,
        CreditCard,
        PayPal,
        BankTransfer
    }

    public interface IPaymentMethod
    {
        EPaymentMethod NameOfPaymentMethod { get; }
        public string AccountID { get; set; }
        public string UserName { get; set; }
        public void ProcessPayment(double amount);
    }

    public class CreditCardPayment : IPaymentMethod
    {
        public EPaymentMethod NameOfPaymentMethod
        {
            get { return EPaymentMethod.CreditCard; }
        }

        public string AccountID { get; set; }
        public string UserName { get; set; }

        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"\nProcessing credit card payment of {amount:C} with credit card number {AccountID}");
            return;
        }
    }

    public class PayPalPayment : IPaymentMethod
    {
        public EPaymentMethod NameOfPaymentMethod
        {
            get { return EPaymentMethod.PayPal; }
        }

        public string AccountID { get; set; }
        public string UserName { get; set; }

        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"\nProcessing PayPal payment of {amount:C} with username {AccountID}");
            return;
        }
    }

    public class BankTransferPayment : IPaymentMethod
    {
        public EPaymentMethod NameOfPaymentMethod
        {
            get { return EPaymentMethod.BankTransfer; }
        }
        public string AccountID { get; set; }
        public string UserName { get; set; }

        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"\nProcessing bank transfer payment of {amount:C} with bank account number {AccountID}");
            return ;
        }
    }

    public class Payment
    {
        public IPaymentMethod PaymentMethod;

        public Payment()
        {

        }


        public void ProcessPayment(double amount)
        {
            PaymentMethod.ProcessPayment(amount);
        }
    }
    public class Invoice
    {
        private static int invoiceCount = 0;
        public string InvoiceNumber { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public int Quantity { get; private set; } = 0;
        public Payment Payment { get; set; } = null;
        public Product Product { get; set; } = null;
        
        public Invoice()
        {
            //Auto Generate invoice number and date
            invoiceCount++;
            InvoiceNumber = $"INV{DateTime.Now:yyyyMMdd}--{invoiceCount}:Bootcamp";
            InvoiceDate = DateTime.Now;
        }

        public void CreateInvoice(int qty, Product product, Payment payment)
        {
            Quantity = qty;
            Product = product;
            Payment = payment;
        }

        public void PrintInvoice()
        {
            Console.WriteLine("\nDetail Invoice USING FACADE");
            Console.WriteLine($"- Invoice Number: {InvoiceNumber}");
            Console.WriteLine($"- Invoice Date: {InvoiceDate.ToShortDateString()}");
            Console.WriteLine($"- Product: {Product.Name}");
            Console.WriteLine($"- Quantity: {Quantity}");
            Console.WriteLine($"- Price: {Product.Price:C}");
            Console.WriteLine($"- Total Amount: {Quantity*Product.Price:C}");
            Console.WriteLine($"- Payment Method: {Payment.PaymentMethod.NameOfPaymentMethod}");
            Console.WriteLine($"\t- Account ID {Payment.PaymentMethod.AccountID}");
            Console.WriteLine($"\t- UserName : {Payment.PaymentMethod.UserName}");
        }
    }

    public class Order
    {
        private Product _product;
        private Payment _payment;
        private Invoice _invoice;

        public Order()
        {
            this._product = new Product();
            this._payment = new Payment();
            this._invoice = new Invoice();
        }

        public void CreateOrder(string ProdcutName,int qty, int Price, string accountID, string userName,EPaymentMethod paymentMethodType)
        {
            _product.Name=ProdcutName;
            _product.Price=Price;
            //_product.GetProductDetails();
             IPaymentMethod paymentMethod = ChoosePaymentMethod(paymentMethodType,accountID, userName);
            _payment.PaymentMethod=paymentMethod;
            _payment.ProcessPayment(_product.Price * qty);
            _invoice.CreateInvoice(qty, _product, _payment);
            _invoice.PrintInvoice();
        }

        private IPaymentMethod? ChoosePaymentMethod(EPaymentMethod paymentMethodType, string accountID, string userName)
        {
            IPaymentMethod paymentMethod;
            switch (paymentMethodType)
            {
                case EPaymentMethod.CreditCard:
                    paymentMethod = new CreditCardPayment();
                    paymentMethod.AccountID = accountID;
                    paymentMethod.UserName = userName;
                    break;
                case EPaymentMethod.PayPal:
                    paymentMethod = new PayPalPayment();
                    paymentMethod.AccountID = accountID;
                    paymentMethod.UserName = userName;
                    break;
                case EPaymentMethod.BankTransfer:
                    paymentMethod = new BankTransferPayment();
                    paymentMethod.AccountID = accountID;
                    paymentMethod.UserName = userName;
                    break;
                default:
                    Console.WriteLine("Payment Method not found");
                    paymentMethod=null; 
                    break;
            }

            return paymentMethod;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var Order = new Order ();
            Order.CreateOrder("Laptop",2,5000000,"12345","John",EPaymentMethod.CreditCard);
            Console.ReadLine();
        }
    }
}
