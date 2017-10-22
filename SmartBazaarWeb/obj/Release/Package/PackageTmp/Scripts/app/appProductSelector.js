var appProductSelector = function () {
    var self = this;

    self.apiUrl = "/Admin/CatalogManager/JIndex";
    self.perpage = 10;
    
    self.ajax = function (param, success) {
        $.ajax({
            type: "GET",
            url: self.apiUrl,
            data: param,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: success,
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status);
                console.log(thrownError);
            }
        });
    }

    self.allItems = ko.observableArray(null);
    
    self.selectitem = function (data) {
        window.selectorApi.add(data.Id, data.Title);
    }

    self.totalPage = ko.computed(function () {
        var div = Math.floor(self.allItems().length / self.perpage);
        div += self.allItems().length % self.perpage > 0 ? 1 : 0;
        return div > 1 ? div : 1;
    });
    self.pagenr = ko.observable(0);
    self.hasNext = ko.computed(function () {
        return self.pagenr() < self.totalPage() - 1 && 1 < self.totalPage();
    });
    self.hasPrev = ko.computed(function () {
        return self.pagenr() !== 0;
    });
    self.next = function () {
        if (self.hasNext())
            self.pagenr(self.pagenr() + 1);
    }
    self.prev = function () {
        if (self.hasPrev())
            self.pagenr(self.pagenr() - 1);
    }
    self.items = ko.computed(function () {
        return self.allItems().slice(self.pagenr() * self.perpage, (self.pagenr() + 1) * self.perpage);
    });

    self.app = {
        construct: function () {
            $("#btnProductList").click(function () {
                var term = $("#txtProductSearch").val();
                self.app.list(term);
            });
        },
        list: function (term) {
            self.ajax({
                q: term
            }, function (data) {
                self.allItems(data);
            });
        }
    }

    return {
        init: function () {
            ko.applyBindings(self, document.getElementById("divProductSelector"));
            self.app.construct();
        }
    }

}();

$(document).ready(function () {
    appProductSelector.init();
});