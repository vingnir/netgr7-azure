function GenerateQRCode(guid) {
    let qrcode = new QRCode(document.getElementById(`${guid}-qrcode`));
    let url = "https://netgr7-blazorapp.azurewebsites.net/devices/add/" + guid;
    qrcode.makeCode(url);
}