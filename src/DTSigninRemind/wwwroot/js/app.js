$(function () {
    $(document).on("pageInit", function (e) {
        $("#picker1").picker({
            toolbarTemplate: '<header class="bar bar-nav">\
  <a class="button button-link pull-right close-picker">确定</a>\
  <h1 class="title"></h1>\
  </header>',
            cols: [
              {
                  textAlign: 'center',
                  values: ['关闭', '提前5分钟', '提前6分钟', '提前7分钟', '提前8分钟', '提前9分钟', '提前10分钟', '提前11分钟', '提前12分钟', '提前13分钟', '提前14分钟', '提前15分钟', '提前16分钟', '提前17分钟', '提前18分钟', '提前19分钟', '提前20分钟', '提前21分钟', '提前22分钟', '提前23分钟', '提前24分钟', '提前25分钟', '提前26分钟', '提前27分钟', '提前28分钟', '提前29分钟', '提前30分钟'],
                  displayValues: ['关闭', '5分钟', '6分钟', '7分钟', '8分钟', '9分钟', '10分钟', '11分钟', '12分钟', '13分钟', '14分钟', '15分钟', '16分钟', '17分钟', '18分钟', '19分钟', '20分钟', '21分钟', '22分钟', '23分钟', '24分钟', '25分钟', '26分钟', '27分钟', '28分钟', '29分钟', '30分钟'],

              }
            ]
        });
        $("#picker2").picker({
            toolbarTemplate: '<header class="bar bar-nav">\
  <a class="button button-link pull-right close-picker">确定</a>\
  <h1 class="title"></h1>\
  </header>',
            cols: [
              {
                  textAlign: 'center',
                  values: ['关闭', '延迟5分钟', '延迟6分钟', '延迟7分钟', '延迟8分钟', '延迟9分钟', '延迟10分钟', '延迟11分钟', '延迟12分钟', '延迟13分钟', '延迟14分钟', '延迟15分钟', '延迟16分钟', '延迟17分钟', '延迟18分钟', '延迟19分钟', '延迟20分钟', '延迟21分钟', '延迟22分钟', '延迟23分钟', '延迟24分钟', '延迟25分钟', '延迟26分钟', '延迟27分钟', '延迟28分钟', '延迟29分钟', '延迟30分钟'],
                  displayValues: ['关闭', '5分钟', '6分钟', '7分钟', '8分钟', '9分钟', '10分钟', '11分钟', '12分钟', '13分钟', '14分钟', '15分钟', '16分钟', '17分钟', '18分钟', '19分钟', '20分钟', '21分钟', '22分钟', '23分钟', '24分钟', '25分钟', '26分钟', '27分钟', '28分钟', '29分钟', '30分钟'],

              }
            ]
        });
    });
    $(document).on("pageAnimationStart", function (e, pageId, $page) {


        if (pageId == "router3") {
            $.ajax({
                url: "/SigninRemind/GetDtConfig",
                async: true,
                type: "GET",
                contentType: "application/json",
                success: function (data, status, xhr) {
                    _corpid = data.corpId;
                    _userid = data.userId;
                    dd.config({
                        agentId: data.agentId,
                        corpId: data.corpId,
                        timeStamp: data.timeStamp,
                        nonceStr: data.nonceStr,
                        signature: data.signature,
                        debug: true,
                        jsApiList: ['runtime.info',
                    'device.notification.confirm',
                    'device.notification.alert',
                    'biz.navigation.setTitle',
                    'biz.telephone.call'
                        ]
                    });
                    dd.ready(function () {

                        //设置导航栏标题
                        dd.biz.navigation.setTitle({
                            title: '使用帮助',
                            onSuccess: function (result) {
                            },
                            onFail: function (err) {
                                $.alert(JSON.stringify(err));
                            }
                        });
                        //设置导航问号图标
                        dd.biz.navigation.setIcon({
                            showIcon: false,//是否显示icon
                            iconIndex: 1,//显示的iconIndex,如上图
                            onSuccess: function (result) {
                                /*结构
                                {
                                }*/
                                //点击icon之后将会回调这个函数
                            },
                            onFail: function (err) {
                                //jsapi调用失败将会回调此函数
                                $.alert(JSON.stringify(err));
                            }
                        });
                        //设置左侧导航栏
                        dd.biz.navigation.setLeft({
                            show: true,//控制按钮显示， true 显示， false 隐藏， 默认true
                            control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
                            showIcon: false,//是否显示icon，true 显示， false 不显示，默认true； 注：具体UI以客户端为准
                            text: '',//控制显示文本，空字符串表示显示默认文本
                            onSuccess: function (result) {
                                /*
                                {}
                                */
                                //如果control为true，则onSuccess将在发生按钮点击事件被回调
                                $.router.load("/SigninRemind?id=dinga43facadf38c3669&dd_nav_bgcolor=FF5E97F6", true)
                            },
                            onFail: function (err) { $.alert(JSON.stringify(err)); }
                        });
                    });

                    dd.error(function (err) {
                        $.alert(JSON.stringify(err));
                    });
                }
            });

        }
        else if (pageId == "router4") {
            $.ajax({
                url: "/SigninRemind/GetDtConfig",
                async: true,
                type: "GET",
                contentType: "application/json",
                success: function (data, status, xhr) {
                    dd.config({
                        agentId: data.agentId,
                        corpId: data.corpId,
                        timeStamp: data.timeStamp,
                        nonceStr: data.nonceStr,
                        signature: data.signature,
                        debug: true,
                        jsApiList: ['runtime.info',
                    'device.notification.confirm',
                    'device.notification.alert',
                    'biz.navigation.setTitle',
                    'biz.telephone.call'
                        ]
                    });
                    dd.ready(function () {
                        //设置导航栏标题
                        dd.biz.navigation.setTitle({
                            title: '统计',
                            onSuccess: function (result) {
                            },
                            onFail: function (err) {
                                $.alert(JSON.stringify(err));
                            }
                        });
                        //设置导航问号图标
                        dd.biz.navigation.setIcon({
                            showIcon: false,//是否显示icon
                            iconIndex: 1,//显示的iconIndex,如上图
                            onSuccess: function (result) {
                                /*结构
                                {
                                }*/
                                //点击icon之后将会回调这个函数
                            },
                            onFail: function (err) {
                                //jsapi调用失败将会回调此函数
                                $.alert(JSON.stringify(err));
                            }
                        });
                        //设置左侧导航栏
                        dd.biz.navigation.setLeft({
                            show: true,//控制按钮显示， true 显示， false 隐藏， 默认true
                            control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
                            showIcon: false,//是否显示icon，true 显示， false 不显示，默认true； 注：具体UI以客户端为准
                            text: '',//控制显示文本，空字符串表示显示默认文本
                            onSuccess: function (result) {
                                //如果control为true，则onSuccess将在发生按钮点击事件被回调
                                $.router.load("/SigninRemind?id=dinga43facadf38c3669&dd_nav_bgcolor=FF5E97F6", true)
                            },
                            onFail: function (err) { $.alert(JSON.stringify(err)); }
                        });

                    });

                    dd.error(function (err) {
                        $.alert(JSON.stringify(err));
                    });

                }
            });
        }
        else if (pageId == "router5") {
            dd.ready(function () {
                //禁用下拉刷新
                //设置导航栏标题
                dd.biz.navigation.setTitle({
                    title: '注册人员',
                    onSuccess: function (result) {
                    },
                    onFail: function (err) {
                        $.alert(JSON.stringify(err));
                    }
                });
                //设置导航问号图标
                dd.biz.navigation.setIcon({
                    showIcon: false,//是否显示icon
                    iconIndex: 1,//显示的iconIndex,如上图
                    onSuccess: function (result) {
                        /*结构
                        {
                        }*/
                        //点击icon之后将会回调这个函数
                    },
                    onFail: function (err) {
                        //jsapi调用失败将会回调此函数
                        $.alert(JSON.stringify(err));
                    }
                });

                lastIndex = $('.list-container li').length;
                if (lastIndex >= maxItems) {
                    // 加载完毕，则注销无限加载事件，以防不必要的加载
                    $.detachInfiniteScroll($('.infinite-scroll'));
                    // 删除加载提示符
                    $('.infinite-scroll-preloader').remove();
                    return;
                }
                $.ajax({
                    url: "/SigninRemind/SigninRemindList?pageIndex=" + _pageindex + "&pageSize=" + itemsPerLoad,
                    async: true,
                    type: "GET",
                    contentType: "application/json",
                    success: function (data, status, xhr) {
                        maxItems = data.signinRemindCount;
                        usercountdata = data.signinRemindList;
                        //预先加载20条
                        addItems(itemsPerLoad, _pageindex, usercountdata);
                    }
                });




            });

            dd.error(function (err) {
                $.alert(JSON.stringify(err));
            });

        }
        else if (pageId == "router6") {
            dd.ready(function () {
                //禁用下拉刷新
                //设置导航栏标题
                dd.biz.navigation.setTitle({
                    title: '每日一句',
                    onSuccess: function (result) {
                    },
                    onFail: function (err) {
                        $.alert(JSON.stringify(err));
                    }
                });
                //设置导航问号图标
                dd.biz.navigation.setIcon({
                    showIcon: false,//是否显示icon
                    iconIndex: 1,//显示的iconIndex,如上图
                    onSuccess: function (result) {
                        /*结构
                        {
                        }*/
                        //点击icon之后将会回调这个函数
                    },
                    onFail: function (err) {
                        //jsapi调用失败将会回调此函数
                        $.alert(JSON.stringify(err));
                    }
                });
                //设置左侧导航栏
                dd.biz.navigation.setLeft({
                    show: true,//控制按钮显示， true 显示， false 隐藏， 默认true
                    control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
                    showIcon: false,//是否显示icon，true 显示， false 不显示，默认true； 注：具体UI以客户端为准
                    text: '',//控制显示文本，空字符串表示显示默认文本
                    onSuccess: function (result) {
                        /*
                        {}
                        */
                        //如果control为true，则onSuccess将在发生按钮点击事件被回调
                        $.router.load("/SigninRemind?id=dinga43facadf38c3669&dd_nav_bgcolor=FF5E97F6", true)
                    },
                    onFail: function (err) { $.alert(JSON.stringify(err)); }
                });
            });

            dd.error(function (err) {
                $.alert(JSON.stringify(err));
            });
        }


    });
    // 添加'refresh'监听器
    $(document).on('refresh', '.pull-to-refresh-content.pull-to-refresh-echart', function (e) {
        // 模拟2s的加载过程
        setTimeout(function () {
            $.ajax({
                url: "/SigninRemind/GetRecentPush",
                async: true,
                type: "GET",
                contentType: "application/json",
                success: function (data1, status, xhr) {
                    document.getElementById("totalNumber").textContent = data1.TotalNumber;
                    document.getElementById("runPush").textContent = data1.RunPush;
                    document.getElementById("stopPush").textContent = data1.StopPush;
                    document.getElementById("totalPushMsg").textContent = data1.TotalPushMsg;
                    document.getElementById("pushWorkMsg").textContent = data1.PushWorkMsg;
                    document.getElementById("pushOffMsg").textContent = data1.PushOffMsg;
                    document.getElementById("todayPushMsg").textContent = data1.TodayPushMsg;
                    document.getElementById("yesterdayPushMsg").textContent = data1.YesterdayPushMsg;
                    document.getElementById("recentPush").textContent = data1.RecentPush;
                    document.getElementById("enableApp").textContent = data1.EnableApp;
                    document.getElementById("enableMsg").textContent = data1.EnableMsg;
                    document.getElementById("todayPushAppMsg").textContent = data1.TodayPushAppMsg;
                    document.getElementById("todayPushSmsMsg").textContent = data1.TodayPushSmsMsg;
                    document.getElementById("yesterdayPushAppMsg").textContent = data1.YesterdayPushAppMsg;
                    document.getElementById("yesterdayPushSmsMsg").textContent = data1.YesterdayPushSmsMsg;

                }
            });
            // 加载完毕需要重置
            $.pullToRefreshDone('.pull-to-refresh-content.pull-to-refresh-echart');
        }, 1000);
    });


    $(document).on('click', ".my-btn1", function () {
        var clod = $.closePanel("#panel-right-demo");

        $.router.load("/SigninRemind/AdvSettings", true);

    });

    // 注册'infinite'事件处理函数
    $(document).on('infinite', '.infinite-scroll-bottom', function () {

        // 如果正在加载，则退出
        if (loading) return;

        // 设置flag
        loading = true;

        // 模拟1s的加载过程
        setTimeout(function () {
            // 重置加载flag
            loading = false;
            lastIndex = $('.list-container li').length;
            if (lastIndex >= maxItems) {
                // 加载完毕，则注销无限加载事件，以防不必要的加载
                $.detachInfiniteScroll($('.infinite-scroll'));
                // 删除加载提示符
                $('.infinite-scroll-preloader').remove();
                return;
            }

            //if (maxItems - lastIndex < 20)
            //{
            //    itemsPerLoad = maxItems - lastIndex;
            //    alert(itemsPerLoad);
            //}

            $.ajax({
                url: "/SigninRemind/SigninRemindList?pageIndex=" + _pageindex + "&pageSize=" + itemsPerLoad,
                async: true,
                type: "GET",
                contentType: "application/json",
                success: function (data, status, xhr) {
                    //$.alert(data.signinRemindCount);
                    maxItems = data.signinRemindCount;
                    usercountdata = data.signinRemindList;
                    //预先加载20条
                    addItems(itemsPerLoad, _pageindex, usercountdata);
                }
            });
            // 添加新条目
            //addItems(itemsPerLoad, lastIndex);
            // 更新最后加载的序号

            lastIndex = $('.list-container li').length;

            //容器发生改变,如果是js滚动，需要刷新滚动
            $.refreshScroller();
        }, 1000);
    });

    $.init();
});

function showApp() {
    document.getElementById("button1").className = "button button-round active";
    document.getElementById("button2").className = "button button-round";
    document.getElementById("remindmode").value = "App";
};
function showMsg() {
    document.getElementById("button1").className = "button button-round";
    document.getElementById("button2").className = "button button-round active";
    document.getElementById("remindmode").value = "Msg";
    //$.alert('短信功能暂时不可用！');
};

function runsetTitle() {
    //设置导航栏标题
    dd.biz.navigation.setTitle({
        title: '使用帮助',
        onSuccess: function (result) {
        },
        onFail: function (err) { }
    });
};

function runddconfig(agentId, corpId, timeStamp, nonceStr, signature) {
    dd.config({
        agentId: '' + agentId + '',
        corpId: '' + corpId + '',
        timeStamp: timeStamp,
        nonceStr: '' + nonceStr + '',
        signature: '' + signature + '',
        jsApiList: ['runtime.info',
    'device.notification.confirm',
    'device.notification.alert',
    'biz.navigation.setTitle',
    'biz.telephone.call'
        ]
    });
};
function showtelephone() {
    dd.biz.telephone.call({
        users: ['' + _userid + ''], //用户列表，工号
        corpId: '' + _corpid + '', //企业id
        onSuccess: function () { },
        onFail: function () { }
    });
}

var _corpid, _userid;

// 加载flag
var loading = false;
// 最多可加载的条目
var maxItems = 100;

// 每次加载添加多少条目
var itemsPerLoad = 20;
// 上次加载的序号

var lastIndex = 20;

var _pageindex = 1;

var usercountdata;

function addItems(number, lastIndex, userdata) {
    // 生成新条目的HTML

    var html = '';
    for (var i = 0 ; i < lastIndex + number - lastIndex; i++) {
        if (JSON.stringify(userdata[i]) == null) {
            break;
        }
        else {
            //if (userdata[i].avatar == null)
               // $.alert("ssssssss");
            html += '<li>';
            html += '<a href="/SigninRemind/UserContent?userid=' + userdata[i].userid + '" class="item-link item-content">';
            html += '<div class="item-media"><img class="img-circle" src="' + (userdata[i].avatar != null ? (userdata[i].avatar + '_250x250q60.jpg') : 'http://gqianniu.alicdn.com/bao/uploaded/i4//tfscom/i3/TB10LfcHFXXXXXKXpXXXXXXXXXX_!!0-item_pic.jpg') + '" style="width: 4rem;"></div>';
            html += '<div class="item-inner">';
            html += '<div class="item-title-row">';
            html += '<div class="item-title">' + userdata[i].name + '</div>';
            html += '<div class="item-after"><input type="checkbox" ' + (userdata[i].isEnable == false ? '' : 'checked') + ' readonly></input></div>';
            html += '</div>';
            html += '<div class="item-subtitle">' + (userdata[i].position != null ? userdata[i].position : null) + '</div>';
            html += '<div class="item-text">' + userdata[i].regDateTime + '</div>';
            html += '</div></a></li>';

           // html += '<li class="item-content"><div class="item-inner"><div class="item-title">Item ' + userdata[i].userid + '</div></div></li>';
        }
    }
    // 添加新条目
    $('.infinite-scroll-bottom .list-container').append(html);
    _pageindex = _pageindex + 1;
}

