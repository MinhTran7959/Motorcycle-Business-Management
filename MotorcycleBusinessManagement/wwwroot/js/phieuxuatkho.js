//$(document).ready(function () {
//    var totalGianhap = 0;
//    var itongtienhang = $('#itongtienhang');

//    $('input[type="checkbox"].chkSelect').on('change', function () {
//        var checkbox = $(this);
//        var idct = checkbox.data('idct');
//        var soluong = checkbox.data('soluong');
//        var gianhap = checkbox.data('gianhap');
//        var rowIndex = checkbox.data('rowindex');
//        //var mapnInput = $('#detailTable input.mapn');
//        //var tenxeInput = $('#detailTable input.tenxe');
//        //var tenmxInput = $('#detailTable input.tenmx');
//        //var gianhapInput = $('#detailTable input.gianhap');
//        //var biensolucmuaInput = $('#detailTable input.biensolucmua');


//        if (checkbox.is(':checked')) {
//            soluong = 0;
//            totalGianhap += gianhap;

//            $.ajax({
//                type: 'POST',
//                url: '/Phieuxuats/GetIDCT',
//                data: { idct: idct },
//                success: function (result) {
//                    console.log("Idct được chọn: " + idct);
//                },
//                error: function (error) {
//                    console.error('Error:', error);
//                }
//            });

//            //mapnInput.val(checkbox.closest('tr').find('td:eq(2)').text().trim());
//            //tenxeInput.val(checkbox.closest('tr').find('td:eq(3)').text().trim());
//            //tenmxInput.val(checkbox.closest('tr').find('td:eq(4)').text().trim());
//            //gianhapInput.val(checkbox.data('gianhap'));
//            //biensolucmuaInput.val(checkbox.closest('tr').find('td:eq(6)').text().trim());
//            var newRow = $('#detailTable tr:first').clone();
//            newRow.removeAttr('style');
//            $('#detailTable').append(newRow);


//            // Cập nhật các input của dòng mới
//            newRow.find('input.mapn').val(checkbox.closest('tr').find('td:eq(2)').text().trim());
//            newRow.find('input.tenxe').val(checkbox.closest('tr').find('td:eq(3)').text().trim());
//            newRow.find('input.tenmx').val(checkbox.closest('tr').find('td:eq(4)').text().trim());
//            newRow.find('input.sokhung').val(checkbox.closest('tr').find('td:eq(5)').text().trim());
//            newRow.find('input.somay').val(checkbox.closest('tr').find('td:eq(6)').text().trim());
//            //newRow.find('input.gianhap').val(gianhap);
//            newRow.find('input.gianhap').val(parseFloat(gianhap).toLocaleString('vi-VN'));
//            newRow.find('input.biensolucmua').val(checkbox.closest('tr').find('td:eq(8)').text().trim());
//        } else {
//            soluong = checkbox.data('soluong');
//            totalGianhap -= gianhap;
//            console.log("Idct bị bỏ chọn: " + idct);
//            $('#detailTable tr:last').remove();
//            $.ajax({
//                type: 'POST',
//                url: '/Phieuxuats/GetIDCT',
//                data: { idct: idct },
//                success: function (result) {
//                    console.log("Idct được chọn: " + idct);
//                },
//                error: function (error) {
//                    console.error('Error:', error);
//                }
//            });
//        }

//        itongtienhang.val(totalGianhap.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
//        console.log("Soluong của hàng " + rowIndex + " là: " + soluong);
//    });
//});


$(document).on('change', '.idct-dropdown', function () {
    // Sử dụng $(this).find(":selected") để lấy option đã được chọn
    var selectedOption = $(this).find(":selected");
    var row = $(this).closest('tr');

    var idpnk = selectedOption.data('idpnk');
    var idxe = selectedOption.data('idxe');
    var idmx = selectedOption.data('idmx');
    var doixe = selectedOption.data('doixe');
    var sokhung = selectedOption.data('sokhung');
    var somay = selectedOption.data('somay');
    var giaban = selectedOption.data('giaban');
    var gianhap = selectedOption.data('gianhap');
    var trangthai = selectedOption.data('trangthai');
    var biensolucmua = selectedOption.data('biensolucmua');
    var biensolucban = selectedOption.data('biensolucban');
    var thoigianbaohanh = selectedOption.data('thoigianbaohanh');

    var formattedGiaban = parseFloat(giaban).toLocaleString('vi-VN').replace(/\./g, ',');
    row.find('.Giaban').val(formattedGiaban);
    row.find('.Idpnk').val(idpnk);
    row.find('.Idxe').val(idxe);
    row.find('.Idmx').val(idmx);
    row.find('.Doixe').val(doixe);
    row.find('.Gianhap').val(gianhap);
    row.find('.Trangthai').val(trangthai);
    row.find('.Biensolucmua').val(biensolucmua);
    row.find('.Thoigianbaohanh').val(thoigianbaohanh);
    row.find('.Sokhung').val(sokhung);
    row.find('.Somay').val(somay);
    row.find('.Biensolucban').val(biensolucban);

    console.log(idpnk);
    console.log(idxe);
    console.log(idmx);
    console.log(gianhap);
    console.log(trangthai);
    console.log(biensolucmua);
    console.log(thoigianbaohanh);
    var isDuplicate = checkDuplicateIdct(selectedOption.val(), this);
    //$.ajax({
    //    type: 'POST',
    //    url: '/Phieuxuats/GetIDCT',
    //    data: {
    //        idct: selectedOption.val(),
    //        sokhung: sokhung,
    //        somay: somay,
    //        giaban: giaban,
    //        biensolucban: biensolucban
    //    },
    //    success: function (result) {
    //        console.log("Idct đã được chọn: " + selectedOption.val());
    //        // Xử lý kết quả thành công nếu cần
    //    },
    //    error: function (error) {
    //        console.error('Error:', error);
    //        // Xử lý lỗi nếu cần
    //    }
    //});
    if (!isDuplicate) {
        // Nếu không trùng lặp, cập nhật tổng tiền
        updateTotal();
    }
});


function DeleteItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    var visibleRowCount = 0;
    for (var i = 1; i < rows.length; i++) {
        if (rows[i].style.display !== 'none') {
            visibleRowCount++;
        }
    }

    if (visibleRowCount == 1) {
        return;
    }
    else {
        $(btn).closest('tr').hide();
        // Gọi hàm Xoadonghide() để cập nhật giá trị
        Xoadonghide();
    }

    var row = $(btn).closest('tr');
    var gianhapInput = row.find('input[id^="Gianhap-"]');
    gianhapInput.val(0);

    // Gọi hàm formatCurrencyAndTotal() để định dạng lại giá trị và cập nhật tổng tiền
    formatCurrencyAndTotal(gianhapInput[0]);
    updateTotal();
    // Ẩn hàng trong bảng
    row.hide();
}


function AddItem(btn) {

    var table;
    table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');
    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = rows.length - 2;

    var nextrowIdx = eval(lastrowIdx) + 1;

    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    var ixacnhanInput = newRow.querySelector('input[id^="ixacnhan-"]');
    if (ixacnhanInput) {
        ixacnhanInput.value = 'true';
    }

    rebindvalidators();

}

function rebindvalidators() {
    var $form = $("#ApplicantForm");

    $form.unbind();

    $form.data("validator", null);

    $.validator.unobtrusive.parse($form);

    $form.validate($form.data("unobtrusiveValidation").options);
}
function Xoadonghide() {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    for (var i = 1; i < rows.length; i++) {
        var row = rows[i];
        var isHidden = row.style.display === 'none';
        var xacnhanInput = row.querySelector('input[id^="ixacnhan-"]');

        if (xacnhanInput) {
            xacnhanInput.value = isHidden ? 'false' : 'true';
        }
    }
}

function formatCurrency(input) {

    var value = input.value.replace(/\D/g, '');
    var formatter = new Intl.NumberFormat('vi-VN');
    var formattedValue = formatter.format(value);

    // Thay thế dấu "." bằng dấu ","
    formattedValue = formattedValue.replace(/\./g, ',');

    input.value = formattedValue;

    // Cập nhật tổng tiền hàng
    Tongtienhang();
    updateTotal();
}
function formatCurrencyAndTotal(input) {
    formatCurrency(input);

    var total = 0;
    var ithanhtienElements = document.querySelectorAll('[id^="Giaban-"]');

    ithanhtienElements.forEach(function (element) {
        var ithanhtien = parseFloat(element.value.replace(/\D/g, '')) || 0;
        total += ithanhtien;
    });

    var itongtienhang = document.getElementById("itongtienhang");
    if (itongtienhang) {
        var formatter = new Intl.NumberFormat('vi-VN');
        var formattedTotal = formatter.format(total);
        formattedTotal = formattedTotal.replace(/\./g, ',');

        itongtienhang.value = formattedTotal;
    }
}

//function submitForm() {
//    document.getElementById("ApplicantForm").submit();
//}

function Tongtienhang() {
    var total = 0;

    var ithanhtienElements = document.querySelectorAll('[id^="Giaban-"]');

    ithanhtienElements.forEach(function (element) {
        var ithanhtien = parseFloat(element.value.replace(/\D/g, '')) || 0;
        total += ithanhtien;
    });

    var itongtienhang = document.getElementById("itongtienhang");
    if (itongtienhang) {
        var formatter = new Intl.NumberFormat('vi-VN');
        var formattedTotal = formatter.format(total);
        formattedTotal = formattedTotal.replace(/\./g, ',');

        itongtienhang.value = formattedTotal;
    }
}
window.onload = Tongtienhang;

//function Kiemtradongia(element) {
//    var row = element.id.split('-')[1];

//    // Lấy giá trị từ các trường cần kiểm tra
//    var idongiaValue = $('#Giaban-' + row).val();
//    var Idct = $('#Idct-' + row).val();

//    // Xóa dấu phẩy (,) trong giá trị
//    var sodongiaValue = idongiaValue.replace(/,/g, '');

//    // Kiểm tra xem có phải là số không
//    if (isNaN(sodongiaValue)) {
//        console.log('Giá bán không hợp lệ.');
//        return;
//    }

//    $.ajax({
//        url: '/Phieuxuats/Kiemtradongia',
//        type: 'GET',
//        dataType: 'json',
//        data: { Gianhap: parseFloat(sodongiaValue), idct: Idct },
//        success: function (result) {
//            if (result <= 0) {
//                // Giá nhập hợp lệ
//                $('#Gianhap-' + row).addClass('is-valid').removeClass('is-invalid');
//                $('#Gianhap-' + row).popover('dispose');
//                $('#Gianhap-' + row).popover({
//                    html: true,
//                    title: 'Thông báo',
//                    content: 'Giá bán hợp lệ.',
//                    trigger: 'hover',
//                    placement: 'bottom',
//                    template: '<div class="popover" role="tooltip"><h3 class="popover-header text-success"></h3><div class="popover-body bg-success text-white"></div></div>'
//                });
//            } else {
//                // Giá nhập lỗ
//                $('#Giaban-' + row).popover('dispose');
//                $('#Giaban-' + row).addClass('is-invalid').removeClass('is-valid');
//                var formatter = new Intl.NumberFormat('en-US');
//                var formattedValue = formatter.format(result);
//                $('#Giaban-' + row).popover({
//                    html: true,
//                    title: 'Thông báo',
//                    content: 'Giá bán bị lỗ ' + formattedValue + 'đ.',
//                    trigger: 'hover',
//                    placement: 'bottom',
//                    template: '<div class="popover" role="tooltip"><h3 class="popover-header text-danger"></h3><div class="popover-body bg-danger text-white"></div></div>'
//                });
//            }
//        },
//        error: function () {
//            console.log('Lỗi Ajax request!');
//        }
//    });
//}

function Kiemtradongia(element) {
    var row = element.id.split('-')[1];

    // Lấy giá trị từ các trường cần kiểm tra
    var selectedOption = $('#Idct-' + row + ' option:selected');
    var gianhapValue = parseFloat(selectedOption.data('gianhap'));
    var giabanValue = parseFloat(element.value.replace(/,/g, ''));
    
    // Kiểm tra giá nhập và giá bán
    if (gianhapValue > giabanValue) {
        // Giá nhập lớn hơn giá bán
        $('#Giaban-' + row).addClass('is-invalid').removeClass('is-valid');
        toastr.error('Giá bán bị lỗ ' + (gianhapValue - giabanValue).toLocaleString('vi-VN') + 'đ.');
        document.querySelector('.btnSubmit').disabled = true;
    } else {
        // Giá nhập nhỏ hơn hoặc bằng giá bán
        $('#Giaban-' + row).removeClass('is-invalid').addClass('is-valid');
        toastr.success('Giá bán hợp lệ.');
        document.querySelector('.btnSubmit').disabled = false;
    }
}

//check chọn trùng xe
function showToast(message, isError = false) {
    // Tạo một toast-container nếu chưa tồn tại
    var toastContainer = document.getElementById('toast-container');
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.id = 'toast-container';
        toastContainer.classList.add('position-fixed', 'bottom-0', 'end-0', 'p-3');
        document.body.appendChild(toastContainer);
    }

    // Tạo một toast mới
    var newToast = document.createElement('div');
    newToast.classList.add('toast');
    newToast.setAttribute('role', 'alert');
    newToast.setAttribute('aria-live', 'assertive');
    newToast.setAttribute('aria-atomic', 'true');

    // Thêm nội dung toast
    newToast.innerHTML = `
            <div class="toast-header ${isError ? 'bg-danger' : 'bg-success'}">
                <strong class="me-auto">Thông báo</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">${message}</div>
        `;

    // Thêm toast vào container
    toastContainer.appendChild(newToast);

    // Hiển thị toast
    var bootstrapToast = new bootstrap.Toast(newToast);
    bootstrapToast.show();
}


//check trùng Idct
function checkDuplicateIdct(selectedIdct, element) {
    // Lấy tất cả các dropdown có class 'idct-dropdown'
    var idctDropdowns = document.getElementsByClassName('idct-dropdown');

    // Biến kiểm tra trùng lặp
    var isDuplicate = false;

    // Lặp qua từng dropdown
    for (var i = 0; i < idctDropdowns.length; i++) {
        // Lấy giá trị Idct của dropdown hiện tại
        var currentIdct = idctDropdowns[i].value;

        // Kiểm tra xem Idct đã được chọn trước đó và có trùng với Idct hiện tại hay không
        if (currentIdct === selectedIdct && isDuplicate) {
            showToast('Xe đã được chọn trước đó ở một dòng khác. Vui lòng chọn xe khác', true);

            // Reset giá trị của dropdown nếu trùng
            element.value = "";
            updateTotal(); // Cập nhật giá tiền khi trùng
            
            return false; // Ngăn chặn thực hiện hàm JS
        } else if (currentIdct === selectedIdct) {
            // Nếu chưa trùng lặp và Idct đã được chọn trước đó, đặt isDuplicate thành true
            isDuplicate = true;

            // Cập nhật tổng tiền ngay khi Idct được chọn
            updateTotal();
        }
    }

    // Kiểm tra xem Idct đã bị xóa không
    if (!element.value) {
        isDuplicate = false;
        
    } 

    return isDuplicate; // Cho phép thực hiện hàm JS
}




function updateTotal() {
    var total = 0;
    var ithanhtienElements = document.querySelectorAll('[id^="Giaban-"]');

    ithanhtienElements.forEach(function (element) {
        var ithanhtien = parseFloat(element.value.replace(/\D/g, '')) || 0;
        total += ithanhtien;
    });

    var itongtienhang = document.getElementById("itongtienhang");
    if (itongtienhang) {
        var formatter = new Intl.NumberFormat('vi-VN');
        var formattedTotal = formatter.format(total);
        formattedTotal = formattedTotal.replace(/\./g, ',');

        itongtienhang.value = formattedTotal;
    }
}
