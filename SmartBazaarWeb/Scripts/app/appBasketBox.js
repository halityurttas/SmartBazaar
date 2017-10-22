var appBasketBox = function () {
    var self = this;
    
    self.BasketCount = ko.observable('');
    self.BasketTotal = ko.observable('');

    self.BasketDetail = ko.observableArray([]);

    self.app = {
        construct: function () {
            self.app.summary();
            self.app.detail();
        },
        detail: function () {
            $.ajax({
                url: "/Basket/GetBasketDetailApi"
            }).done(function (data) {
                self.BasketDetail(data);
            });
        },
        summary: function () {
            $.ajax({
                url: "/Basket/GetBasketSummaryApi"
            }).done(function (data) {
                self.BasketCount(data.BasketCount);
                self.BasketTotal(data.BasketTotal);
            });
        },
        addBasket: function (id, cnt) {
            $.ajax({
                url: "/Basket/AddItem",
                data: { ProductId: id, Quantity: cnt }
            }).done(function (data) {
                if (data.Success) {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                } else {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                }
                self.app.detail();
                self.app.summary();
            });
        },
        addRelatedBasket: function (id, relid, cnt, cmp) {
            $.ajax({
                url: "/Basket/AddRelatedItem",
                data: { ProductId: id, RelatedProductId: relid, Quantity: cnt, CampaignId: cmp }
            }).done(function (data) {
                if (data.Success) {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                } else {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                }
                self.app.detail();
                self.app.summary();
            });
        },
        removeBasket: function (id) {

        },
        removeAll: function () {
            $.ajax({
                url: "/Basket/RemoveAll"
            }).done(function (data) {
                if (data.Success) {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                } else {
                    $("#basketMessageData").html(data.Message);
                    $("#basketMessage").modal('show');
                }
                self.app.detail();
                self.app.summary();
            });
        }
    }

    return {
        init: function () {
            ko.applyBindings(this, document.getElementById('basketbox'));
            self.app.construct();
        },
        refresh: function () {
            console.log("call refresh");
            self.app.construct();
        },
        addBasket: function (id, cnt) {
            self.app.addBasket(id, cnt);
        },
        addRelatedBasket: function (id, relid, cnt, cmp) {
            self.app.addRelatedBasket(id, relid, cnt, cmp);
        },
        removeBasket: function (id) {
            self.app.removeBasket(id);
        },
        removeAll: function () {
            self.app.removeAll();
        }
    }
}();