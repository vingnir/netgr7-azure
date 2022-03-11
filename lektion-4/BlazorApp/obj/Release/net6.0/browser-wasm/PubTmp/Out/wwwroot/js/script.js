function GenerateQRCode(guid) {
    let qrcode = new QRCode(document.getElementById(`${guid}-qrcode`));
    let url = "/devices/add/" + guid;
    qrcode.makeCode(url);
}