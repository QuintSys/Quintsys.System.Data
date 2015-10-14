using System;
using System.Data;
using Moq;
using NUnit.Framework;
using Quintsys.System.Data;

namespace Tests
{
    [TestFixture]
    public class DataReaderExtensionsTests
    {
        private Mock<IDataReader> _moq;

        [SetUp]
        public void Init()
        {
            bool readToggle = true;

            _moq = new Mock<IDataReader>();
            _moq.Setup(x => x.Read())
                .Returns(() => readToggle)
                .Callback(() => readToggle = false);

            _moq.Setup(x => x["columnName"]).Returns(1);
            _moq.Setup(x => x[0]).Returns(1);
        }

        [Test]
        public void Can_Get_Column_Value_By_ColumnName()
        {
            IDataReader dataReader = _moq.Object;

            int value = dataReader.ColumnValue<int>(columnName: "columnName");

            Assert.NotNull(value);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Can_Get_Column_Value_By_ColumnPosition()
        {
            IDataReader dataReader = _moq.Object;

            int value = dataReader.ColumnValue<int>(columnIndex: 0);

            Assert.NotNull(value);
            Assert.AreEqual(expected: 1, actual: value);
        }

        [Test]
        public void Can_Get_Single_Record_Value()
        {
            IDataReader dataReader = _moq.Object;

            int value = dataReader.Single(dr => dr.ColumnValue<int>("columnName"));

            Assert.NotNull(value);
            Assert.AreEqual(expected: 1, actual: value);
        }

        [Test]
        public void Should_Raise_If_Getting_Single_Value_From_Multiple_Results_DataReader()
        {
            Mock<IDataReader> moq = new Mock<IDataReader>();
            moq.Setup(x => x["columnName"]).Returns(1);
            moq.Setup(x => x.Read()).Returns(() => true); // simulates infinite records
            IDataReader dataReader = moq.Object;

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => dataReader.Single(dr => dr.ColumnValue<int>("columnName")));
            Assert.That(actual: ex.Message, expression: Is.EqualTo("Multiple rows returned!"));
        }
    }
}