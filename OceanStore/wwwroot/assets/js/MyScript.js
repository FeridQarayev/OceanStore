let items = $(".nav .nav-item.menu-items");
for (var i = 0; i < items.length; i++) {
    let linkButton = items[i].children[0].href.split('/')
    let linkWindow = window.location.pathname;
    //console.log("btnLink", '/'+ linkButton[3] +'/'+ linkButton[4])
    //console.log("window", linkWindow)
    if (('/' + linkButton[3] + '/' + linkButton[4])==linkWindow) {
        items[i].children[0].classList.add("my_active")
    }
}