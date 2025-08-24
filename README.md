# Clearbank Tech Test

## Things to refactor

- PaymentService utlises AppSettings to decide on what Database to use. That responsibility shouldnt be down to the PaymentService. Also with appSettings being used it implies it can be changed without restarting the application.


- Switch case for PaymentSchemes would need to be split and each of the PaymentSchemes logic should be encased in their own validator class.


- Deductions Should be handled seperately from the class too.


- PaymentService should just care about determining how to Make a payment and return a result.