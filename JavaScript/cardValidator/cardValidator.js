function isCreditCardValid(input) {

    var cardNumberLength = 19;
    var minimumSumOfCardNumbers = 16;
    var result = {};

    result.valid = false;
    result.number = input;

    if (isWrongLength(input, cardNumberLength)) {
        return invalidResult(result, "wrong_length");
    }

    else {
        var isInputFormatCorrect = isFormatCorrect(input);

        if (!isInputFormatCorrect) {
            return invalidResult(result, "invalid format. Should be XXXX-XXXX-XXXX-XXXX");
        }
        else {
            var numbers = getNumbersFromInput(input);

            if (isLastNumberOdd(numbers)) {
                return invalidResult(result, "The last digit must be even");
            }

            if (isSumOfNumbersLessThen16(numbers, minimumSumOfCardNumbers)) {
                return invalidResult(result, "The sum of all the digits must be greater than 16");
            }

            if (isAllDuplicates(numbers)) {
                return invalidResult("All the digits cannot be the same");
            }

            return returnCorrectResult(result, input);
        }
    }
}


function isWrongLength(input, cardNumberLength) {
    return input.length != cardNumberLength;
}

function isFormatCorrect(input) {
    var regexInputFormat = /\d{4}-\d{4}-\d{4}-\d{4}/;
    return regexInputFormat.test(input);
}

function getNumbersFromInput(input) {
    var regexNumbers = /\d/g;
    return input.match(regexNumbers);
}

function isLastNumberOdd(numbers) {
    return numbers[numbers.length - 1] % 2 != 0;
}

function isSumOfNumbersLessThen16(numbers, minimumSumOfCardNumbers) {
    var sumOfNumbers = numbers.reduce(function (memo, value) {
        return +memo + +value;
    }, 0);

    if (sumOfNumbers <= minimumSumOfCardNumbers)
        return true;
}

function isAllDuplicates(numbers) {
    for (var i = 0; i < numbers.length - 1; i++) {
        if (numbers[i] != numbers[i + 1])
            return false;
    }
    return true;
}

function invalidResult(result, error) {
    result.error = error;
    return result;
}

function returnCorrectResult(result, number) {
    result.valid = true;
    result.number = number;
    return result;
}
