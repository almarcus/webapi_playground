# webapi_playground

## Deploying to AWS Lambda

1. Configure the AWS CLI
2. `cd WebAPI`
3. Run `dotnet lambda deploy-function`
4. When setting up a new Lambda Function give the following parameters:
   1. Function Name
   2. Existing IAM Role or New Role
      1. If new, give name and select the `AWSLambdaExecute` IAM Policy
   3. Navigate to the function URL and see the "Hello World!" call
   4. Navigate to the `/weatherforecast` endpoint to see a controller method being called
   5. If desired, set the `function-name` in the `aws-lambda-tools-defaults.json` file