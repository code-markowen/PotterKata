using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PotterKata
{
	[TestFixture]
	public class UnitTest1
	{
		[Test]
		public void OneFirstBookCosts8Euros()
		{
			var shoppingBasket = new ShoppingBasket();
			shoppingBasket.Add(new FirstBook());
			var total = shoppingBasket.Total();
			var expected = new Euros(8);
			Assert.That(total, Is.EqualTo(expected));
		}


		[Test]
		public void TwoFirstBooksCosts16Euros()
		{
			var shoppingBasket = new ShoppingBasket();
			shoppingBasket.Add(new FirstBook());
			shoppingBasket.Add(new FirstBook());
			var total = shoppingBasket.Total();
			var expected = new Euros(16);
			Assert.That(total, Is.EqualTo(expected));
		}

		[Test]
		public void OneFirstBookAndOneSecondBookCosts15Euros20()
		{
			var shoppingBasket = new ShoppingBasket();
			shoppingBasket.Add(new FirstBook());
			shoppingBasket.Add(new SecondBook());
			var total = shoppingBasket.Total();
			var expected = new Euros(15.20);

			Assert.That(total, Is.EqualTo(expected));
		}
	}

	public class Euros
	{
		private readonly double _amount;

		public Euros(double amount)
		{
			_amount = amount;
		}

		public override bool Equals(object obj)
		{
			return _amount == ((Euros) obj)._amount;
		}

		public override string ToString()
		{
			return _amount.ToString();
		}
	}

	public class FirstBook : Book
	{
	}

	public class SecondBook : Book
	{
	}

	public abstract class Book
	{
		
	}

	public class PercentageDiscount
	{
		private readonly double _percentage;

		public PercentageDiscount(double percentage)
		{
			_percentage =  percentage;
		}


		public double Calculate(double total)
		{
			return total * _percentage/100;
		}
	}

	public class ShoppingBasket
	{
		private double _total;
		private IList<Book> _books = new List<Book>();
		private double BOOK_PRICE = 8;

		public Euros Total()
		{
			return new Euros(_total);
		}

		public void Add(Book book)
		{
			_books.Add(book);
			_total += BOOK_PRICE;
			if (ShouldApplyDiscount(book))
			{
				ApplyDiscount(new PercentageDiscount(5));
			}
		}

		private bool ShouldApplyDiscount(Book book)
		{
			return (_books.Any(x => x.GetType() != book.GetType()));
		}

		private void ApplyDiscount(PercentageDiscount percentageDiscount)
		{
			_total -= percentageDiscount.Calculate(_total);
		}
	}
}
