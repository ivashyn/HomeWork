function isCreditCardValid(input) {
    
    var result = {};
    result.valid = false;
    result.number = input;

    if(input.length != 19) {
        invalidResult(result,"wrong_length");
        return false;
    }

    else {
        var regexInputFormat = /\d{4}-\d{4}-\d{4}-\d{4}/;
        var isFormatCorrect = regexInputFormat.test(input);

        if(!isFormatCorrect) {
            invalidResult(result, "invalid format. Should be XXXX-XXXX-XXXX-XXXX");
            return false;
        }
        else {
            var regexNumbers = /\d/g;
            var numbers = input.match(regexNumbers);

            if(numbers[numbers.length-1] % 2 != 0){
                invalidResult(result,"The last digit must be even");
                return false;
            }

            var sumOfNumbers = numbers.reduce(function(memo, value) {
                return +memo + +value;
            }, 0);

            if(sumOfNumbers<=16) {
                invalidResult(result, "The sum of all the digits must be greater than 16");
                return false;
            }
                
            if(isAllDuplicates(numbers)) {
                invalidResult("All the digits cannot be the same");
                return false;
            }

            returnCorrectResult(result, input)
        } 
    }
}

function isAllDuplicates(numbers) {
    for (var i = 0; i < numbers.length-1; i++) {
        if(numbers[i] != numbers[i+1])
            return false;
    }
    return true;
}

function invalidResult(result,error) {
    result.error = error;
    console.log(result);
}

function returnCorrectResult(result, number) {
    result.valid = true;
    result.number = number;
    console.log(result);
}
