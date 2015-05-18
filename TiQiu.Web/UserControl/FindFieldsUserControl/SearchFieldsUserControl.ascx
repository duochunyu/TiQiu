<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFieldsUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.FindFieldsUserControl.SearchFieldsUserControl" %>
<div class="clearfix" style="">
    <div class="form-layout" style="margin-left: 15px; width: 550px; float: left;">
        <h2 style="font-size: 14px; border-bottom: 1px solid #000; padding-bottom: 5px; margin-bottom: 10px;">搜索条件</h2>
        <p>
            <label class="lable-normal">球场名词：</label><input class="input_text" id="Text1" type="text" />
            <label class="lable-normal">区域：</label><select id="Select1">
                <option selected="selected">武侯区</option>
                 <option>成华区</option>
                 <option>锦江区</option>
                 <option>高新区</option>

            </select>
            <label for="RadbuttonFive">
                <input id="RadbuttonFive" name="typeField" type="radio" />
                5人制</label>
            <label for="RadbuttonEle">
                <input id="RadbuttonEle" name="typeField" type="radio" />
                11人制</label>
        </p>
        <p>
            <label class="lable-normal">有空时间段：</label>
            <input class="input_text" id="Text2" type="text" />
            至 <input class="input_text" id="Text3" type="text" />
        </p>
        <div class="hr"></div>
        <p>高级搜索</p>
        <p>
            <label class="">价格：</label><input class="input_text" id="Text4" type="text" />
            --
                        <input class="input_text" id="Text5" type="text" />
            <lable>￥</lable>
        </p>
        <p>
            <label>
                <input id="Checkbox1" type="checkbox" />
                澡堂</label>
        </p>
        <div class="hr"></div>
        <p class="align-center">
            <a href="#">
                <img src="../../Images/search-btn.png"/>
            </a>
        </p>
    </div>
</div>
