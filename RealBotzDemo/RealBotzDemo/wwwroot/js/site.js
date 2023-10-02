
var today = new Date().toISOString().split('T')[0];
document.getElementById('DateOfBirth').max = today;

//Submit record
function fnSubmit(event) {
    event.preventDefault();
    if (!$("form").valid()) {
        return false;
    }
    var fav = [];
    var favNames = [];
    $.each($('input:checkbox[name="hobbies"]:checked'), function () {
        fav.push($(this).val());
        favNames.push($(this).attr('hobbyName'));
    });

    let Id = $('#hdn_Id').val();
    let Name = $('#Name').val();
    let DateOfBirth = $('#DateOfBirth').val();
    let Address = $('#Address').val();
    let Email = $('#Email').val();
    let CountryId = $('#CountryId').val();
    let Gender = $('#Gender').val();
    let Hobby = fav.join(",");
    let HobbyNames = favNames.join(",");

    $.ajax({
        type: "POST",
        url: "/Home/CrudOperation/",
        data: {
            Id: Id,
            Name: Name,
            DateOfBirth: DateOfBirth,
            Address: Address,
            Email: Email,
            CountryId: CountryId,
            Gender: Gender,
            Hobby: Hobby,
            HobbyNames: HobbyNames,
        },
        success: function (data) {
            alert(data.message);
        },
        error: function (response) {
            console.log(response);
        },
        complete: function () {
            fnLoadList();
            fnClearFormData();
        },
    });
}

//Bind grid list
function fnLoadList() {
    $.ajax({
        type: "POST",
        url: "/Home/GetList",
        data: {},
        success: function (data) {
            $('#divListPartial').html(data);
        },
        error: function (response) {
            console.log(response);
        }
    });
}
fnLoadList();

//Clear form after operation complete
function fnClearFormData() {
    $('#hdn_Id').val(0);
    $('#btnSubmit').text("Submit");

    $('#Name').val('');
    $('#DateOfBirth').val('');
    $('#Address').val('');
    $('#Email').val('');
    $('#CountryId').val('');

    $.each($('input:checkbox[name="hobbies"]:checked'), function () {
        $(this).prop("checked", false);
    });

    $('#Gender').val('');
}

//Delete Record
function fnDeleteData(Id) {
    if (window.confirm("Are you sure you want to delete this record?")) {
        $.ajax({
            type: "GET",
            url: "/Home/Delete",
            data: {
                Id: Id,
            },
            success: function (data) {
                alert(data.message);
            },
            error: function (response) {
                console.log(response);
            },
            complete: function () {
                fnLoadList();
            }
        });
    }
}

//Get Data by Id and fill up form
function fnEditData(Id) {
    $.ajax({
        type: "POST",
        url: "/Home/GetDataById",
        data: {
            Id : Id
        },
        success: function (response) {

            if (response.statusCode == 1) {
                var data = response.data;

                $('#hdn_Id').val(data.id);
                $('#btnSubmit').text("Update");

                $('#Name').val(data.name);

                $('#DateOfBirth').val(data.dob);

                $('#Address').val(data.address);
                $('#Email').val(data.email);
                $('#CountryId').val(data.countryId);

                let splhobby = data.hobby.split(',');
                for (var i = 0; i < splhobby.length; i++) {
                    let hoobyId = splhobby[i].trim();
                    $("#chk_" + hoobyId).prop("checked", true);
                }

                $('#Gender').val(data.gender);
            }
            else {
                alert(response.message);
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

