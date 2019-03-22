using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CartTracker.UnitTests.Builders
{
    public class SqlExceptionBuilder
    {
        private int errorNumber;
        private string errorMessage;

        public SqlExceptionBuilder WithErrorNumber(int errorNumber)
        {
            this.errorNumber = errorNumber;

            return this;
        }

        public SqlExceptionBuilder WithErrorMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;

            return this;
        }

        public SqlException Builder()
        {
            var sqlError = CreateError();
            var sqlErrorCollection = CreateErrorCollection(sqlError);
            var sqlException = CreateException(sqlErrorCollection);

            return sqlException;
        }

        private SqlError CreateError()
        {
            var constructors = typeof(SqlError).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var firstSqlErrorConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 8);
            
            
            
            SqlError sqlError = firstSqlErrorConstructor?.Invoke(
                new object[]
                {
                    this.errorNumber,
                    new byte(),
                    new byte(),
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    new int(),
                    null
                }) as SqlError;

            return sqlError;
        }

        private SqlErrorCollection CreateErrorCollection(SqlError sqlError)
        {
            // Create instance via Reflection
            var constructors = typeof(SqlErrorCollection).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var firstConstructor = constructors.FirstOrDefault();
            SqlErrorCollection errorCollection = firstConstructor?.Invoke(new object[] { }) as SqlErrorCollection;
            
            // Add the error
            typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(errorCollection, new object[] {sqlError});

            return errorCollection;
        }

        private SqlException CreateException(SqlErrorCollection errorCollection)
        {
            if (errorCollection == null)
            {
                return null;
            }

            var constructors = typeof(SqlException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var firstConstructor = constructors.FirstOrDefault();
            
            SqlException sqlException = firstConstructor?.Invoke(
                new object[]
                {
                    this.errorMessage,
                    errorCollection,
                    null,
                    Guid.NewGuid()
                }) as SqlException;

            return sqlException;
        }
    }
}