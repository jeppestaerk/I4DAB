var ViewModel = function () {
    var self = this;
    self.people = ko.observableArray();
    self.error = ko.observable();
    self.newPerson = {
        Adresse: ko.observable(),
        Telefons: ko.observable(),
        AlternativAdresses: ko.observable(),
        PersonType: ko.observable(),
        Efternavn: ko.observable(),
        Mellemnavn: ko.observable(),
        Fornavn: ko.observable()
     }

    var peopleUri = 'api/People/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllPeople() {
        ajaxHelper(peopleUri, 'GET').done(function (data) {
            self.books(data);
        });
    }

    self.addPerson = function(formElement) {
        var person = {
            AdresseID: self.newPerson.Adresse().Id,
            Fornavn: self.newPerson.Fornavn(),
            Mellemnavn: self.newPerson.Mellemnavn(),
            Efternavn: self.newPerson.Efternavn(),
            PersonType: self.newPerson.PersonType()
        };

        ajaxHelper(peopleUri, 'POST', person)
            .done(function(item) {
                self.people.push(item);
            });
    }
    // Fetch the initial data.
    getAllPeople();
};

ko.applyBindings(new ViewModel());