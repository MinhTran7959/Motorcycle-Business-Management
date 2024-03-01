
$(document).on('change', '.idpnk-dropdown', function () {
    // Sử dụng $(this).find(":selected") để lấy option đã được chọn
    var selectedOption = $(this).find(":selected");
    var row = $(this).closest('tr');

    var sohd = selectedOption.data('sohd');
    var ngayhdStr = selectedOption.data('ngayhd');
    var idncc = selectedOption.data('idncc');
    var idnv = selectedOption.data('idnv');
    var tenncc = selectedOption.data('tenncc');
    var tennv = selectedOption.data('tennv');
    var tongtien = selectedOption.data('tongtien');
    var ngaynhapStr = selectedOption.data('ngaynhap');
    var mapnk = selectedOption.data('mapnk');

    var formattedTongtien = parseFloat(tongtien).toLocaleString('vi-VN').replace(/\./g, ',');
    //// Định dạng ngày nhập thành "dd/mm/yyyy"
    /*var formattedNgaynhap = moment(ngaynhap).format('YYYY-MM-DD');*/
    var ngaynhap = new Date(ngaynhapStr);

    // Định dạng ngày nhập thành "dd/mm/yyyy"
    var formattedNgaynhap = ngaynhap.toLocaleDateString('en-IN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    }).split('/').reverse().join('-');
    var ngayhd = new Date(ngayhdStr);

    // Định dạng ngày nhập thành "dd/mm/yyyy"
    var formattedNgayhd = ngayhd.toLocaleDateString('en-IN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    }).split('/').reverse().join('-');
    row.find('.Tongtiennhap').val(formattedTongtien);
    row.find('.Sohd').val(sohd);
    row.find('.Idncc').val(idncc);
    row.find('.Idnv').val(idnv);
    row.find('.Tenncc').val(tenncc);
    row.find('.Tennv').val(tennv);
    row.find('.Ngayhd').val(formattedNgayhd);
    /*row.find('.Ngaynhap').val(ngaynhap);*/
    row.find('.Ngaynhap').val(formattedNgaynhap);
    row.find('.Mapnk').val(mapnk);

    var isDuplicate = checkDuplicateIdct(selectedOption.val(), this);

    console.log(formattedNgaynhap);

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
    var gianhapInput = row.find('input[id^="Tongtiennhap-"]');
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
    var ithanhtienElements = document.querySelectorAll('[id^="Tongtiennhap-"]');

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


function Tongtienhang() {
    var total = 0;

    var ithanhtienElements = document.querySelectorAll('[id^="Tongtiennhap-"]');

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
    var idctDropdowns = document.getElementsByClassName('idpnk-dropdown');

    // Biến kiểm tra trùng lặp
    var isDuplicate = false;

    // Lặp qua từng dropdown
    for (var i = 0; i < idctDropdowns.length; i++) {
        // Lấy giá trị Idct của dropdown hiện tại
        var currentIdct = idctDropdowns[i].value;

        // Kiểm tra xem Idct đã được chọn trước đó và có trùng với Idct hiện tại hay không
        if (currentIdct === selectedIdct && isDuplicate) {
            showToast('Phiếu nhập kho đã được chọn trước đó ở một dòng khác. Vui lòng chọn xe khác', true);

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
    var ithanhtienElements = document.querySelectorAll('[id^="Tongtiennhap-"]');

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
