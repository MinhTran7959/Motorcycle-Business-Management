function HideModal() {
    $('myModal').modal('hide');
}

function EditModal(Idacc) {
    $.ajax({
        url: 'Accounts/Edit=' + Idacc,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Không thể đọc dữ liệu');
            }
            else if (response.length == 0) {
                alert('Dữ liệu không khả thi với id = ' + Idacc);
            }
        }
    });
}