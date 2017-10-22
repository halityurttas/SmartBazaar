$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

$.validator.methods.date = function (value, element) {
    var dateRegex = /^(0?[1-9]\.|[12]\d\.|3[01]\.){2}(19|20)\d\d$/;
    return this.optional(element) || dateRegex.test(value);
};