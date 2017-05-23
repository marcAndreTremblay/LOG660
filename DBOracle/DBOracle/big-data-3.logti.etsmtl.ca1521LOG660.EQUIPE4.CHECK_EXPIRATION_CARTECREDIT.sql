-- ****** Object: Table Trigger EQUIPE4.CHECK_EXPIRATION_CARTECREDIT Script Date: 2017-05-23 10:40:51 ******
CREATE TRIGGER "CHECK_EXPIRATION_CARTECREDIT"
  BEFORE
  INSERT OR UPDATE OF "DATEEXPIRATION"
  ON "CARTECREDIT"
  FOR EACH ROW
  WHEN (NEW.DateExpiration > sysdate)
DECLARE
  -- Declare constants and variables in this section.
  -- Example: <Variable Identifier> <DATATYPE>
  --          <Variable Identifier> CONSTANT <DATATYPE>
  --          varEname  VARCHAR2(40);
  --          varSalary CONSTANT NUMBER:=1000;
  --          varSalaryDiff NUMBER;
  --          invalid_salary EXCEPTION;
BEGIN -- executable part starts here
  -- Write PL/SQL and SQL statements to implement the processing logic
  -- Example: Restricting EMP table update through UPDATE Trigger ON TABLE
  --          varSalaryDiff = ((:new.SAL - :old.SAL)/:old.SAL)*100;
  --          IF (varSalaryDiff < 0 || varSalaryDiff > 20) THEN
  --            RAISE invalid_salary;
  --          END IF;
  NULL;
  -- EXCEPTION -- exception-handling part starts here
  -- WHEN invalid_salary THEN
  --   dbms_output.put_line('Updated salary is not within the range');
END;
