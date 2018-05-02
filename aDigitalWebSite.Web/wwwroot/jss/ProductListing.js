function GetAllProducts(){
    $.ajax({
        method:"GET",
        url:productServiceRootUrl,
        success:MapProducts
    })
}

function MapProducts(products){
    var model = $("#cardTemplate");
    var container = model.parent();
    products.forEach(function(data){
        var mapped = MapSingleProduct(data, model);
    })
}

function MapSingleProduct(item, model){
    var newItem = model.clone();
    $("#cardTitle",newItem).text(item.title);
    $("#cardText",newItem).text(item.description);
    $("#cardPrice",newItem).text('A partir de R$'+item.startsAt.toLocaleString('pt-BR',{minimumFractionDigits: 2}) + "/" + item.unitName);
    if(item.minimalAmount){
        $("#cardAmount",newItem).text("Qtd Minima: "+item.minimalAmount+" "+item.unitName); 
    }else{
        $("#cardAmount",newItem).hide();
    }
    $("#productsDiv").append(newItem);
    $(newItem).show();
}