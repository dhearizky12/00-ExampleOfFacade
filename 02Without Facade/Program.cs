namespace WithoutFacade
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
            Console.WriteLine("\nDetail Invoice=WITHOUT FACADE");
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
    class Program
    {
        static void Main(string[] args)
        {
            // create product
            var product1 = new Product();
            product1.Name = "Laptop";
            product1.Price = 5000000;

            // create payment
            var creditCardPayment = new CreditCardPayment();

            // set payment details
            creditCardPayment.AccountID = "1234-5678-9012-3456";
            creditCardPayment.UserName = "John";

            // create payment objects
            var creditCard = new Payment();
            creditCard.PaymentMethod = creditCardPayment;

            //process payment
            int qty = 2;
            double amount = qty * product1.Price;
            creditCard.ProcessPayment(amount);
           

            // create invoice
            var invoice1 = new Invoice();
            invoice1.CreateInvoice(qty, product1, creditCard);


            // print invoice
            invoice1.PrintInvoice();


            Console.ReadLine();
        }
    }
}
