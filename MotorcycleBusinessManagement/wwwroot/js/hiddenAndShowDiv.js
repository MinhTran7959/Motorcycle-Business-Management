document.getElementById('toggle-code-button').addEventListener('click', function () {
    this.style.display = 'block';
    var codeContainer = document.getElementById('code-container');
    if (codeContainer.style.display === 'none') {
        codeContainer.style.display = 'block';
        this.innerHTML = '<i class="fas fa-chevron-down"></i> Mở thêm tài khoản';
    } else {
        codeContainer.style.display = 'none';
        this.innerHTML = '<i class="fas fa-chevron-up"></i> Đóng thêm tài khoản';
    }
});