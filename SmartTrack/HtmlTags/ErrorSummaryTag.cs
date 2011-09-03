using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public class ErrorSummaryTag : HtmlTag
    {
        public ErrorSummaryTag() : base("div")
        {
            this.AddClass("ui-widget")
                .AddClass("ui-error")
                .Style("display", "none")
                .Append(new HtmlTag("div")
                    .AddClass("ui-state-error")
                    .AddClass("ui-corner-all")
                    .Append(new HtmlTag("p")
                        .Append(new HtmlTag("span")
                            .AddClass("ui-icon")
                            .AddClass("ui-icon-alert")
                            .Style("float", "left").Style("margin-right", "0.3em")
                        )
                        .Append(new HtmlTag("span")
                            .AddClass("ui-text")
                            .Text("Validation Error")
                        )
                    )
                    .Append(new HtmlTag("ul"))
                );
        }
        /*
         <div class="ui-widget ui-error" style="display: none;">
              <div class="ui-state-error ui-corner-all">
                <p>
                  <span style="float: left; margin-right: 0.3em;" class="ui-icon ui-icon-alert"></span>
                  <span class="ui-text">An error has occurred. Please contact an administrator.</span>
                </p>
                <ul></ul>
              </div>
            </div>
         */
    }
}