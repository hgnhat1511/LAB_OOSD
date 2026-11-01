import html
import uuid

class DrawIOBuilder:
    def __init__(self, name="Page-1", width=1600, height=1200):
        self.name = name
        self.width = width
        self.height = height
        self.cells = []
        self.next_id = 2

    def _get_id(self):
        cid = str(self.next_id)
        self.next_id += 1
        return cid

    def add_boundary(self, title, x, y, w, h, fill="#f8f9fa", stroke="#495057"):
        cid = self._get_id()
        val = html.escape(title)
        style = (f"swimlane;whiteSpace=wrap;html=1;fillColor={fill};fontColor=#212529;"
                 f"strokeColor={stroke};fontFamily=Times New Roman;fontSize=14;fontStyle=1;startSize=28;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_actor(self, label, x, y, w=40, h=80):
        cid = self._get_id()
        val = html.escape(label)
        style = ("shape=umlActor;verticalLabelPosition=bottom;verticalAlign=top;html=1;"
                 "outlineConnect=0;fillColor=#dae8fc;strokeColor=#6c8ebf;fontFamily=Times New Roman;"
                 "fontSize=12;fontStyle=1;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_usecase(self, label, x, y, w=160, h=60, fill="#e1d5e7", stroke="#9673a6"):
        cid = self._get_id()
        val = html.escape(label)
        style = (f"ellipse;whiteSpace=wrap;html=1;fillColor={fill};strokeColor={stroke};"
                 f"fontFamily=Times New Roman;fontSize=12;fontStyle=1;align=center;verticalAlign=middle;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_class(self, class_name, attributes, methods, x, y, w=200, fill="#dae8fc", stroke="#6c8ebf"):
        cid = self._get_id()
        val = html.escape(class_name)
        h = 32 + max(1, len(attributes)) * 20 + 8 + max(1, len(methods)) * 20
        style = (f"swimlane;fontStyle=1;align=center;verticalAlign=top;childLayout=stackLayout;"
                 f"horizontal=1;startSize=28;horizontalStack=0;resizeParent=1;resizeParentMax=0;"
                 f"resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;fillColor={fill};"
                 f"strokeColor={stroke};fontFamily=Times New Roman;fontSize=13;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        # Attributes
        attr_text = "<br>".join([html.escape(a) for a in attributes]) if attributes else " "
        aid = self._get_id()
        astyle = ("text;strokeColor=none;fillColor=none;align=left;verticalAlign=top;spacingLeft=6;"
                  "spacingRight=4;overflow=hidden;rotatable=0;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;"
                  "whiteSpace=wrap;html=1;fontFamily=Times New Roman;fontSize=11;")
        ah = max(24, len(attributes) * 20)
        self.cells.append(
            f'<mxCell id="{aid}" value="{attr_text}" style="{astyle}" vertex="1" parent="{cid}">'
            f'<mxGeometry y="28" width="{w}" height="{ah}" as="geometry" />'
            f'</mxCell>'
        )
        # Separator line
        lid = self._get_id()
        lstyle = (f"line;strokeWidth=1;fillColor=none;align=left;verticalAlign=middle;spacingTop=-1;"
                  f"spacingLeft=3;spacingRight=3;rotatable=0;labelPosition=right;points=[];portConstraint=eastwest;"
                  f"strokeColor={stroke};")
        self.cells.append(
            f'<mxCell id="{lid}" value="" style="{lstyle}" vertex="1" parent="{cid}">'
            f'<mxGeometry y="{28+ah}" width="{w}" height="8" as="geometry" />'
            f'</mxCell>'
        )
        # Methods
        meth_text = "<br>".join([html.escape(m) for m in methods]) if methods else " "
        mid = self._get_id()
        mh = max(24, len(methods) * 20)
        self.cells.append(
            f'<mxCell id="{mid}" value="{meth_text}" style="{astyle}" vertex="1" parent="{cid}">'
            f'<mxGeometry y="{28+ah+8}" width="{w}" height="{mh}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_activity(self, label, x, y, w=180, h=50, fill="#d5e8d4", stroke="#82b366"):
        cid = self._get_id()
        val = html.escape(label)
        style = (f"rounded=1;whiteSpace=wrap;html=1;arcSize=30;fillColor={fill};strokeColor={stroke};"
                 f"fontFamily=Times New Roman;fontSize=12;fontStyle=1;align=center;verticalAlign=middle;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_initial_node(self, x, y, size=24):
        cid = self._get_id()
        style = "ellipse;fillColor=#000000;strokeColor=none;html=1;"
        self.cells.append(
            f'<mxCell id="{cid}" value="" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{size}" height="{size}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_final_node(self, x, y, size=26):
        cid = self._get_id()
        style = "ellipse;html=1;shape=endState;fillColor=#000000;strokeColor=#d79b00;strokeWidth=2;"
        self.cells.append(
            f'<mxCell id="{cid}" value="" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{size}" height="{size}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_decision(self, label, x, y, w=80, h=60):
        cid = self._get_id()
        val = html.escape(label)
        style = ("rhombus;whiteSpace=wrap;html=1;fillColor=#fff2cc;strokeColor=#d6b656;"
                 "fontFamily=Times New Roman;fontSize=11;fontStyle=1;align=center;verticalAlign=middle;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_state(self, label, x, y, w=140, h=50, fill="#ffe6cc", stroke="#d79b00"):
        cid = self._get_id()
        val = html.escape(label)
        style = (f"rounded=1;whiteSpace=wrap;html=1;arcSize=25;fillColor={fill};strokeColor={stroke};"
                 f"fontFamily=Times New Roman;fontSize=12;fontStyle=1;align=center;verticalAlign=middle;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_lifeline(self, label, x, y=40, w=120, h=500, fill="#dae8fc", stroke="#6c8ebf"):
        cid = self._get_id()
        val = html.escape(label)
        style = (f"shape=umlLifeline;perimeter=lifelinePerimeter;whiteSpace=wrap;html=1;container=1;"
                 f"dropTarget=0;collapsible=0;recursiveResize=0;outlineConnect=0;portConstraint=eastwest;"
                 f"newEdgeStyle={{&quot;curved&quot;:0,&quot;rounded&quot;:0}};fillColor={fill};"
                 f"strokeColor={stroke};fontFamily=Times New Roman;fontSize=12;fontStyle=1;")
        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" vertex="1" parent="1">'
            f'<mxGeometry x="{x}" y="{y}" width="{w}" height="{h}" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def add_relation(self, source_id, target_id, label="", style_type="assoc"):
        cid = self._get_id()
        val = html.escape(label)
        if style_type == "assoc":
            style = "endArrow=none;html=1;rounded=0;strokeColor=#495057;fontFamily=Times New Roman;fontSize=11;"
        elif style_type == "arrow":
            style = "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;endArrow=classic;strokeColor=#212529;fontFamily=Times New Roman;fontSize=11;"
        elif style_type == "include":
            style = "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;dashed=1;endArrow=open;strokeColor=#6c8ebf;fontFamily=Times New Roman;fontSize=10;"
            val = "&amp;lt;&amp;lt;include&amp;gt;&amp;gt;" if not label else f"&amp;lt;&amp;lt;include&amp;gt;&amp;gt;<br>{val}"
        elif style_type == "extend":
            style = "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;dashed=1;endArrow=open;strokeColor=#b85450;fontFamily=Times New Roman;fontSize=10;"
            val = "&amp;lt;&amp;lt;extend&amp;gt;&amp;gt;" if not label else f"&amp;lt;&amp;lt;extend&amp;gt;&amp;gt;<br>{val}"
        elif style_type == "inherit":
            style = "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;endArrow=block;endFill=0;strokeColor=#212529;"
        elif style_type == "return":
            style = "html=1;verticalAlign=bottom;endArrow=open;dashed=1;endSize=8;rounded=0;strokeColor=#495057;fontFamily=Times New Roman;fontSize=11;"
        else:
            style = "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;endArrow=classic;strokeColor=#212529;fontFamily=Times New Roman;fontSize=11;"

        self.cells.append(
            f'<mxCell id="{cid}" value="{val}" style="{style}" edge="1" parent="1" source="{source_id}" target="{target_id}">'
            f'<mxGeometry relative="1" as="geometry" />'
            f'</mxCell>'
        )
        return cid

    def build_xml(self):
        cells_xml = "\n        ".join(self.cells)
        return f"""<mxfile host="app.diagrams.net" modified="2026-11-01T20:00:00.000Z" agent="Antigravity" version="24.0.0" type="device">
  <diagram id="{uuid.uuid4().hex[:16]}" name="{self.name}">
    <mxGraphModel dx="1400" dy="900" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="{self.width}" pageHeight="{self.height}" background="#ffffff" math="0" shadow="0">
      <root>
        <mxCell id="0" />
        <mxCell id="1" parent="0" />
        {cells_xml}
      </root>
    </mxGraphModel>
  </diagram>
</mxfile>"""
