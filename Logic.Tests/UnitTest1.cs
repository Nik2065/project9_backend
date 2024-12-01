namespace Logic.Tests
{
    public partial class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Подготовка к тестированию");

            //если строка подключения не указана, то база создается только в памяти
            _testdb = new DataAccess.DataContext();
        }

        public DataAccess.DataContext _testdb { get; set; }


        [Test]
        public void TestIfDbExists()
        {
            var plist = _testdb.Products.ToList();
        }

        /// <summary>
        /// Тестирвоание поиска по параметрам
        /// </summary>
        [Test]
        public void TestSearchProducts()
        {
            var plist = _testdb.Products.ToList();
        }

    }
}